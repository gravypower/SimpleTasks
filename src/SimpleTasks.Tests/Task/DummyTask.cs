using System;

namespace SimpleTasks.Tests.Task
{
    public class DummyTask:SimpleTasks.Task
    {
        public DummyTask(AbstractTaskContainer<SimpleTasks.Task> taskContainer, string name, Action action, Func<bool> condition) : base(taskContainer, name, action, condition)
        {
        }
    }
}
