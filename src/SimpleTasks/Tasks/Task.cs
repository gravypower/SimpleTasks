using System;

namespace SimpleTasks.Tasks
{
    public class Task:Vertex<Task, TaskContainer>
    {
        public Action Action { get; private set; }

        public Func<bool> Condition { get; }

        public Task(TaskContainer taskContainer, string name, Action action, Func<bool> condition)
        :base(taskContainer, name)
        {
            SetAction(action);
            Condition = condition;
        }

        public Task DependsOn(string name, Action action)
        {
            VertexContainer.Register(name, action);
            VertexContainer.RegisterDependency(name, Name);
            return this;
        }

        public Task DependsOn(Action action)
        {
            var name = ObjectIdGeneratorFacade.GetId(action);
            return DependsOn(name, action);
        }
        
        /// <summary>
        /// Fluent method to create a dependency from this Task onto an <paramref name="action"/>  
        /// </summary>
        /// <param name="action">The Action to be executed</param>
        /// <param name="task">Allows interaction with the newly created task created from the before defined <paramref name="action"/></param>
        /// <returns>The Task</returns>
        public Task DependsOn(Action action, Action<Task> task)
        {
            var name = ObjectIdGeneratorFacade.GetId(action);
            var newTask = VertexContainer.Register(name, action);
            VertexContainer.RegisterDependency(name, Name);

            task.Invoke(newTask);
            
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
    }
}
