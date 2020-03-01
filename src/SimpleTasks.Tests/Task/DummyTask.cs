using System;
using SimpleTasks.Tasks;

namespace SimpleTasks.Tests.Task
{
    public class DummyTask: Tasks.Task
    {
        public DummyTask(Tasks.TaskContainer taskContainer, string name, Action action, Func<bool> condition) : base(taskContainer, name, action, condition)
        {
        }
    }
}
