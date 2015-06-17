using System;
using System.Linq;
using SimpleTasks.Annotations;
using SimpleTasks.Exceptions;

namespace SimpleTasks
{
    public class AbstractTask<TTask> where TTask : AbstractTask<TTask>
    {
        private readonly AbstractTaskContainer<TTask> _taskContainer;

        public string Name { get; private set; }

        public Action Action { get; private set; }

        public bool Invoked { get; private set; }

        public Func<bool> Condition { get; private set; }

        public AbstractTask(AbstractTaskContainer<TTask> taskContainer, string name, Action action, Func<bool> condition)
        {
            _taskContainer = taskContainer;
            Name = name;
            SetAction(action);
            Condition = condition;
        }

        public AbstractTask<TTask> DependsOn(string name, Action action)
        {
            _taskContainer.Register(name, action);
            _taskContainer.RegisterDependicy(name, Name);
            return this;
        }

        public AbstractTask<TTask> DependsOn(object task, Action action)
        {
            var name = ObjectIdGeneratorFacade.GetId(task);
            return DependsOn(name, action);
        }

        public AbstractTask<TTask> DependsOn(params object[] otherTasks)
        {
            foreach (var taskId in otherTasks.Select(ObjectIdGeneratorFacade.GetId))
            {
                if (!_taskContainer.DoesContainTask(taskId))
                    _taskContainer.RegisterEmptyDependicy(taskId);

                DependsOn(taskId);
            }

            return this;
        }

        public AbstractTask<TTask> DependsOn(params string[] otherTasks)
        {
            GuardOtherTasks(otherTasks);

            for (var i = 0; i < otherTasks.Length; i++)
            {
                var otherTask = otherTasks[i];

                GuardOtherTask(otherTask, i);

                if (!_taskContainer.DoesContainTask(otherTask))
                    _taskContainer.RegisterEmptyDependicy(otherTask);

                _taskContainer.RegisterDependicy(otherTask, Name);
            }
            return this;
        }

        private void SetAction(Action action)
        {
            Action = delegate
            {
                action.Invoke();
                Invoked = true;
            };
        }

        [AssertionMethod]
        private static void GuardOtherTasks([NotNull] string[] otherTasks)
        {
            if (otherTasks == null)
                throw new ArgumentNullOrEmptyException("otherTasks");
        }

        [AssertionMethod]
        private void GuardOtherTask(string otherTask, int i)
        {
            if (string.IsNullOrEmpty(otherTask))
                throw new ArgumentNullOrEmptyException("otherTasks at index " + i);

            if (_taskContainer.DoesDependicyExist(otherTask, Name))
                throw new DependicyExistException();
        }
    }
}
