using System;
using FluentAssertions;
using SimpleTasks.Playground.SupportOfTaskTailures.TaskRunnerNotHandlingExceptionsNatively;
using Xunit;

namespace SimpleTasks.Playground.SupportOfTaskTailures
{
    public class Play
    {
        [Fact]
        public void TaskRunnerHandlingExceptionsNatively()
        {
        }

        [Fact]
        public void TaskRunnerNotHandlingExceptionsNatively_TaskHandelsException()
        {
            var container = new TaskRunnerNotHandlingExceptionsNatively.TaskContainer();

            var task = container.Register(ThrowNewException);

            TaskRunnerNotHandlingExceptionsNatively.Task onExceptionTask = null;

            task.OnException += (sender, args) => onExceptionTask = args.Task;

            container.Run();

            onExceptionTask.Should().Be(task);
        }

        [Fact]
        public void TaskRunnerNotHandlingExceptionsNatively_TaskHandelsException2()
        {
            var container = new TaskRunnerNotHandlingExceptionsNatively.TaskContainer();

            var task = container.Register(ThrowNewException, container_OnException);

            container.Run();

            _onExceptionTask.Should().Be(task);
        }

        TaskRunnerNotHandlingExceptionsNatively.Task _onExceptionTask = null;

        private void container_OnException(object sender, TaskExceptionEventArgs eventArgs)
        {
            _onExceptionTask = eventArgs.Task;
        }

        [Fact]
        public void TaskRunnerNotHandlingExceptionsNatively_TaskContainerHandelsExceptions()
        {
            var container = new TaskRunnerNotHandlingExceptionsNatively.TaskContainer();

            var task = container.Register(ThrowNewException);

            TaskRunnerNotHandlingExceptionsNatively.Task onExceptionTask = null;

            container.OnException += (sender, args) => onExceptionTask = args.Task;

            container.Run();

            onExceptionTask.Should().Be(task);
        }

        [Fact]
        public void TaskRunnerNotHandlingExceptionsNatively()
        {
            var container = new TaskRunnerNotHandlingExceptionsNatively.TaskContainer();

            container.Register(ThrowNewException);

            Action act = () => container.Run();

            //Act Assert
            act.Should().Throw<Exception>();
        }


        public void ThrowNewException()
        {
            throw new Exception();
        }
    }
}
