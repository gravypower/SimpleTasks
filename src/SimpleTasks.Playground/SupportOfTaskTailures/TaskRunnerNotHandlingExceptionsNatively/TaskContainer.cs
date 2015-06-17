using System;
using System.Linq;

namespace SimpleTasks.Playground.SupportOfTaskTailures.TaskRunnerNotHandlingExceptionsNatively
{
    public class TaskContainer : AbstractTaskContainer<Task>
    {
        public event TaskExceptionEventHandler OnException;

        protected override void RunTask(Task task)
        {
            try
            {
                task.Action.Invoke();
            }
            catch (Exception ex)
            {
                if (task.DoesOnExceptionHaveSubscribers())
                    task.HandelException(new TaskExceptionEventArgs(ex, task));
                else if (OnException != null && OnException.GetInvocationList().Any())
                    HandelException(new TaskExceptionEventArgs(ex, task));
                else
                    throw;
            }
        }

        public Task Register(Action action, TaskExceptionEventHandler handelException)
        {
            var task = Register(action);
            task.OnException += handelException;

            return task;
        }


        public virtual void HandelException(TaskExceptionEventArgs eventArgs)
        {
            var handler = OnException;
            if (handler != null) handler(this, eventArgs);
        }

        protected override Task ConstructNewTask(string name, Action action, Func<bool> condition)
        {
            return new Task(this, action, name, condition);
        }
    }

    public delegate void TaskContainerExceptionEventHandler(object sender, TaskContainerExceptionEventArgs eventArgs);

    public class TaskContainerExceptionEventArgs
    {
        public Task Task { get; set; }
        public Exception Exception { get; set; }

        public TaskContainerExceptionEventArgs(Exception exception, Task task)
        {
            Task = task;
            Exception = exception;
        }
    }
}