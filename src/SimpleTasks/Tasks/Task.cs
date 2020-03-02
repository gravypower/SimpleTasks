using System;

namespace SimpleTasks.Tasks
{
    public class Task:Vertex<Task, TaskContainer>
    {
        public Action Action { get; private set; }

        public Func<bool> Condition { get; private set; }

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

        public Task DependsOn(object task, Action action)
        {
            var name = ObjectIdGeneratorFacade.GetId(task);
            return DependsOn(name, action);
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
