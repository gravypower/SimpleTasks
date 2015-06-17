using System;

namespace SimpleTasks
{
    public class TaskContainer : AbstractTaskContainer<Task>
    {
        public TaskContainer()
        { }

        public TaskContainer(bool enforceDependencyOnAddOrder):base(enforceDependencyOnAddOrder)
        { }

        protected override void RunTask(Task task)
        {
            task.Action.Invoke();
        }

        protected override Task ConstructNewTask(string name, Action action, Func<bool> condition)
        {
            return new Task(this, name, action, condition);
        }
    }
}
