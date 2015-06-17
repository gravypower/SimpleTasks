using System;
using System.Collections.Generic;
using System.Linq;
using SimpleTasks.Annotations;
using SimpleTasks.Exceptions;
using SimpleTasks.GraphTheory.Graphs;
using SimpleTasks.GraphTheory.Algorithms;

namespace SimpleTasks
{
    public abstract class AbstractTaskContainer<TTask>  where TTask : AbstractTask<TTask>
    {
        private readonly bool _enforceDependencyOnAddOrder;
        private readonly DirectedAcyclicGraph<string> _graph;

        private readonly Stack<TTask> _tasks;

        protected IList<TTask> Tasks
        {
            get { return _tasks.ToList().AsReadOnly(); }
        }

        protected AbstractTaskContainer()
        {
            _tasks = new Stack<TTask>();
            _graph = new DirectedAcyclicGraph<string>();
        }

        public void RegisterDependicy(string source, string target)
        {
            _graph.InsertEdge(source, target);
        }

        protected AbstractTaskContainer(bool enforceDependencyOnAddOrder)
            : this()
        {
            _enforceDependencyOnAddOrder = enforceDependencyOnAddOrder;
        }

        protected abstract void RunTask(TTask task);
        protected abstract TTask ConstructNewTask(string name, Action action, Func<bool> condition);

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

                if(task.Condition != null && !task.Condition())
                    return;

                RunTask(task);
            }
        }

        public TTask Register(Action action)
        {
            return Register(ObjectIdGeneratorFacade.GetId(action.Target), action);
        }

        public TTask Register(string taskName, Action action, Func<bool> condition = null)
        {
            GuardArguments(taskName, action);

            if (_tasks.Any(t => t.Name == taskName))
                throw new TaskExistsException();

            return BuildTask(taskName, action, condition);
        }

        public void RegisterEmptyDependicy(string taskName)
        {
            _graph.InsertVertex(taskName);
        }

        public bool DoesContainTask(string taskName)
        {
            return _graph.Vertices.Contains(taskName);
        }

        private TTask BuildTask(string name, Action action, Func<bool> condition)
        {
            _graph.InsertVertex(name);
            var task = ConstructNewTask(name, action, condition);

            AbstractTask<TTask> lastTask = null;
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
            if (string.IsNullOrEmpty(taskName))
                throw new ArgumentNullOrEmptyException("taskName");

            if (action == null)
                throw new ArgumentNullException("action");
        }
    }
}


