using System;
using System.Collections.Generic;
using System.Linq;
using SimpleTasks.Exceptions;
using SimpleTasks.GraphTheory.Graphs;
using SimpleTasks.GraphTheory.Algorithms;

namespace SimpleTasks
{
    public class TaskContainer : ITaskContainer
    {
        private readonly bool _enforceDependencyOnAddOrder;
        private readonly DirectedAcyclicGraph<string> _graph;

        private readonly Stack<ITask> _tasks;

        protected IList<ITask> Tasks
        {
            get { return _tasks.ToList().AsReadOnly(); }
        }

        public TaskContainer()
        {
            _tasks = new Stack<ITask>();
            _graph = new DirectedAcyclicGraph<string>();
        }

        public void RegisterDependicy(string source, string target)
        {
            _graph.InsertEdge(source, target);
        }

        public TaskContainer(bool enforceDependencyOnAddOrder):this()
        {
            _enforceDependencyOnAddOrder = enforceDependencyOnAddOrder;
        }

        public bool DoesDependicyExist(string source, string target)
        {
            return _graph.Edges.Any(e => e.Source == source && e.Target == target);
        }

        public void Run()
        {
            foreach (var sortedVertex in _graph.TopologicalSort())
            {
                var task = _tasks.SingleOrDefault(t => t.Name == sortedVertex);

                if (task == null)
                    throw new DependicyDoesNotExistException();

                task.Action.Invoke();
            }
        }

        public ITask Register(Action action)
        {
            return Register(ObjectIdGeneratorFacade.GetId(action.Target), action);
        }

        public ITask Register(string taskName, Action action)
        {
            GuardArguments(taskName, action);

            if (_tasks.Any(t => t.Name == taskName))
                throw new TaskExistsException();

            return BuildTask(taskName, action);
        }

        public void RegisterEmptyDependicy(string taskName)
        {
            _graph.InsertVertex(taskName);
        }

        public bool DoesContainTask(string taskName)
        {
            return _graph.Vertices.Contains(taskName);
        }

        private Task BuildTask(string name, Action action)
        {
            _graph.InsertVertex(name);
            var task = new Task(this, action, name);

            ITask lastTask = null;
            if (_enforceDependencyOnAddOrder && Tasks.Any())
                lastTask = _tasks.Peek();

            _tasks.Push(task);

            if (lastTask != null && _enforceDependencyOnAddOrder)
                task.DependsOn(lastTask.Name);

            return task;
        }

        [AssertionMethod]
        private static void GuardArguments([NotNull] string taskName, [NotNull] Action action)
        {
            if (String.IsNullOrEmpty(taskName))
                throw new ArgumentNullOrEmptyException("taskName");

            if (action == null)
                throw new ArgumentNullException("action");
        }
    }
}


