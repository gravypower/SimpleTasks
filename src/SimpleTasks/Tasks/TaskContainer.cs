using System;
using System.Collections.Generic;
using System.Linq;
using SimpleTasks.Annotations;
using SimpleTasks.Exceptions;

namespace SimpleTasks.Tasks
{
    public class TaskContainer : VertexContainer<Task, TaskContainer>
    {
        private readonly Stack<Task> _tasks = new Stack<Task>();
        
        protected IEnumerable<Task> Tasks => _tasks.ToList().AsReadOnly();
        
        public TaskContainer(TaskContainerConfiguration taskContainerConfiguration): base(taskContainerConfiguration)
        {
        }

        public Task Register(Action action)
        {
            return Register(ObjectIdGeneratorFacade.GetId(action.Target), action);
        }

        public Task Register(string taskName, Action action, Func<bool> condition = null)
        {
            GuardArguments(taskName, action);

            if (_tasks.Any(t => t.Name == taskName))
                throw new TaskExistsException();

            return BuildTask(taskName, action, condition);
        }

        private Task BuildTask(string name, Action action, Func<bool> condition)
        {
            Graph.InsertVertex(name);
            var task = new Task(this, name, action, condition);

            _tasks.Push(task);

            if (!ContainerConfiguration.EnforceDependencyOnAddOrder || Tasks.Count() == 1) return task;
            
            var lastTask = _tasks.Peek();
            if(lastTask != null)
                task.DependsOn(lastTask.Name);

            return task;
        }

        [AssertionMethod]
        private static void GuardArguments([NotNull] string taskName, [NotNull] Action action)
        {
            if (string.IsNullOrEmpty(taskName))
                throw new ArgumentNullOrEmptyException("taskName");

            if (action == null)
                throw new ArgumentNullException(nameof(action));
        }

        protected override void DoRun(string vertexName)
        {
            var task = _tasks.SingleOrDefault(t => t.Name == vertexName);

            if (task == null)
                throw new DependencyDoesNotExistException();

            if(task.Condition != null && !task.Condition())
                return;

            task.Action.Invoke();
        }
    }
}
