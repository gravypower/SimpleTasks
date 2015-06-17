using System;
using System.Linq;

namespace SimpleTasks.Playground.SupportOfTaskTailures.TaskRunnerNotHandlingExceptionsNatively
{
    public class Task : AbstractTask<Task>
    {
        public event TaskExceptionEventHandler OnException;

        public Task(ITaskContainer<Task> taskContainer, Action action,
            string name, Func<bool> condition) : base(taskContainer, name, action, condition)
        {
        }

        public virtual void HandelException(TaskExceptionEventArgs eventArgs)
        {
            var handler = OnException;
            if (handler != null) handler(this, eventArgs);
        }

        public bool DoesOnExceptionHaveSubscribers()
        {
            return OnException != null && OnException.GetInvocationList().Any();
        }
    }

    public delegate void TaskExceptionEventHandler(object sender, TaskExceptionEventArgs eventArgs);

    public class TaskExceptionEventArgs
    {
        public Task Task { get; set; }
        public Exception Exception { get; set; }

        public TaskExceptionEventArgs(Exception exception, Task task)
        {
            Task = task;
            Exception = exception;
        }
    }
}
