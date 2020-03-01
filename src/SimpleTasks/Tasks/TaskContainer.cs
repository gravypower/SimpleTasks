﻿using System;
using System.Collections.Generic;
using System.Linq;
using SimpleTasks.Annotations;
using SimpleTasks.Exceptions;

namespace SimpleTasks.Tasks
{
    public class TaskContainer : VertexContainer<Task>
    {
        private TaskContainerConfiguration TaskContainerConfiguration => ContainerConfiguration as TaskContainerConfiguration;

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

        public void RegisterEmptyDependency(string taskName)
        {
            Graph.InsertVertex(taskName);
        }

        private Task BuildTask(string name, Action action, Func<bool> condition)
        {
            Graph.InsertVertex(name);
            var task = new Task(this, name, action, condition);

            var lastTask = default(Task);
            if (_containerConfiguration.EnforceDependencyOnAddOrder && Tasks.Any())
                lastTask = _tasks.Peek();

            _tasks.Push(task);

            if (lastTask != null && _containerConfiguration.EnforceDependencyOnAddOrder)
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
                throw new DependicyDoesNotExistException();

            if(task.Condition != null && !task.Condition())
                return;

            task.Action.Invoke();
        }
    }
}
