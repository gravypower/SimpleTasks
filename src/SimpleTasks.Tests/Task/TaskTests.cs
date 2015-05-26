using System;
using FluentAssertions;
using NUnit.Framework;
using SimpleTasks.Exceptions;

namespace SimpleTasks.Tests.Task
{
    [TestFixture]
    public class TaskTests
    {
        public ITask Sut;

        [SetUp]
        public void SetUp()
        {
            var taskContainer = new SimpleTasks.TaskContainer();
            Sut = taskContainer.Register("SomeTast",  () => GetType());
        }

        [Test]
        public void GivenTask_WhenAddingDependicyOfNull_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn(null);

            //Act Assert
            act.ShouldThrow<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: otherTasks");
        }

        [Test]
        public void GivenTask_WhenAddingDependicyOfEmpty_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn(string.Empty);

            //Act Assert
            act.ShouldThrow<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: otherTasks at index 0");
        }

        [Test]
        public void GivenTask_WhenAddingDependicyOfStringAndNull_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn("SomeTask", default(string));

            //Act Assert
            act.ShouldThrow<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: otherTasks at index 1");
        }

        [Test]
        public void GivenTask_WhenAddingDependicyOfStringAndEmpty_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn("SomeTask", "");

            //Act Assert
            act.ShouldThrow<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: otherTasks at index 1");
        }


        [Test]
        public void GivenTask_WhenAddingSameDependicyTwice_ThenDependicyExistExceptionThrown()
        {
            //Assign
            Sut.DependsOn("SomeName");

            //Act
            Action act = () => Sut.DependsOn("SomeName");

            //Assert
            act.ShouldThrow<DependicyExistException>();
        }
    }
}
