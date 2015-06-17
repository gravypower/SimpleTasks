using System;
using FluentAssertions;
using NUnit.Framework;
using SimpleTasks.Playground.SupportOfTaskTailures.TaskRunnerNotHandlingExceptionsNatively;

namespace SimpleTasks.Playground.SupportOfTaskTailures
{
    [TestFixture]
    public class Play
    {
        [Test]
        public void TaskRunnerHandlingExceptionsNatively()
        {
        }

        [Test]
        public void TaskRunnerNotHandlingExceptionsNatively_TaskHandelsException()
        {
            var container = new TaskRunnerNotHandlingExceptionsNatively.TaskContainer();

            var task = container.Register(ThrowNewException);

            TaskRunnerNotHandlingExceptionsNatively.Task onExceptionTask = null;

            task.OnException += (sender, args) => onExceptionTask = args.Task;

            container.Run();

            onExceptionTask.Should().Be(task);
        }

        [Test]
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

        [Test]
        public void TaskRunnerNotHandlingExceptionsNatively_TaskContainerHandelsExceptions()
        {
            var container = new TaskRunnerNotHandlingExceptionsNatively.TaskContainer();

            var task = container.Register(ThrowNewException);

            TaskRunnerNotHandlingExceptionsNatively.Task onExceptionTask = null;

            container.OnException += (sender, args) => onExceptionTask = args.Task;

            container.Run();

            onExceptionTask.Should().Be(task);
        }

        [Test]
        public void TaskRunnerNotHandlingExceptionsNatively()
        {
            var container = new TaskRunnerNotHandlingExceptionsNatively.TaskContainer();

            container.Register(ThrowNewException);

            Action act = () => container.Run();

            //Act Assert
            act.ShouldThrow<Exception>();
        }


        public void ThrowNewException()
        {
            throw new Exception();
        }
    }
}
