using System;
using System.Linq;
using SimpleTasks.Exceptions;

namespace SimpleTasks
{
    public class Task : ITask
    {
        private readonly ITaskContainer _taskContainer;

        public string Name { get; set; }

        public Action Action { get; private set; }

        public bool Invoked { get; set; }

        public Task(ITaskContainer taskContainer, Action action, string name)
        {
            _taskContainer = taskContainer;
            Name = name;
            SetAction(action);
        }

        public ITask DependsOn(string name, Action action)
        {
            _taskContainer.Register(name, action);
            _taskContainer.RegisterDependicy(name, Name);
            return this;
        }

        public ITask DependsOn(object task, Action action)
        {
            var name = ObjectIdGeneratorFacade.GetId(task);
            return DependsOn(name, action);
        }

        public ITask DependsOn(params object[] otherTasks)
        {
            foreach (var taskId in otherTasks.Select(ObjectIdGeneratorFacade.GetId))
            {
                if (!_taskContainer.DoesContainTask(taskId))
                    _taskContainer.RegisterEmptyDependicy(taskId);

                DependsOn(taskId);
            }

            return this;
        }

        public ITask DependsOn(params string[] otherTasks)
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
