using System;

namespace SimpleTasks
{
    public class Task : AbstractTask<Task>
    {
        public Task(AbstractTaskContainer<Task> taskContainer, string name, Action action, Func<bool> condition)
            : base(taskContainer, name, action, condition)
        {
        }
    }
}
