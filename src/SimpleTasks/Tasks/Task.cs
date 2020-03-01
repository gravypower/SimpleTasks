using System;
using System.Linq;
using SimpleTasks.Annotations;
using SimpleTasks.Exceptions;

namespace SimpleTasks.Tasks
{
    public class Task:Vertex
    {
        private readonly TaskContainer _taskContainer;
        
        public Action Action { get; private set; }

        public Func<bool> Condition { get; private set; }

        public Task(TaskContainer taskContainer, string name, Action action, Func<bool> condition)
        {
            _taskContainer = taskContainer;
            Name = name;
            SetAction(action);
            Condition = condition;
        }

        public Task DependsOn(string name, Action action)
        {
            _taskContainer.Register(name, action);
            _taskContainer.RegisterDependency(name, Name);
            return this;
        }

        public Task DependsOn(object task, Action action)
        {
            var name = ObjectIdGeneratorFacade.GetId(task);
            return DependsOn(name, action);
        }

        public Task DependsOn(params object[] otherTasks)
        {
            foreach (var taskId in otherTasks.Select(ObjectIdGeneratorFacade.GetId))
            {
                if (!_taskContainer.DoesContainVertex(taskId))
                    _taskContainer.RegisterEmptyDependency(taskId);

                DependsOn(taskId);
            }

            return this;
        }

        public Task DependsOn(params string[] otherTasks)
        {
            GuardOtherTasks(otherTasks);

            foreach (var otherTask in otherTasks)
            {
                if (!_taskContainer.DoesContainVertex(otherTask))
                    _taskContainer.RegisterEmptyDependency(otherTask);

                _taskContainer.RegisterDependency(otherTask, Name);
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
        private void GuardOtherTasks([NotNull] string[] otherTasks)
        {
            if (otherTasks == null)
                throw new ArgumentNullOrEmptyException("otherTasks");

            for (var i = 0; i < otherTasks.Length; i++)
            {
                var otherTask = otherTasks[i];
                GuardOtherTask(otherTask, i);
            }
        }

        [AssertionMethod]
        private void GuardOtherTask(string otherTask, int i)
        {
            if (string.IsNullOrEmpty(otherTask))
                throw new ArgumentNullOrEmptyException("otherTasks at index " + i);

            if (_taskContainer.DoesDependencyExist(otherTask, Name))
                throw new DependencyExistException();
        }
    }
}
