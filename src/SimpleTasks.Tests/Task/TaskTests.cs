using System;
using FluentAssertions;
using NUnit.Framework;
using SimpleTasks.Exceptions;

namespace SimpleTasks.Tests.Task
{
    [TestFixture]
    public class TaskTests
    {
        public AbstractTask<SimpleTasks.Task> Sut;

        [SetUp]
        public void SetUp()
        {
            var taskContainer = new SimpleTasks.TaskContainer();
            Sut = taskContainer.Register("SomeTask",  () => GetType());
        }

        [Test]
        public void GivenTask_WhenAddingDependencyOfNull_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn(null);

            //Act Assert
            act.ShouldThrow<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: otherTasks");
        }

        [Test]
        public void GivenTask_WhenAddingDependencyOfEmpty_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn(string.Empty);

            //Act Assert
            act.ShouldThrow<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: otherTasks at index 0");
        }

        [Test]
        public void GivenTask_WhenAddingDependencyOfDefaultString_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn("SomeTask", default(string));

            //Act Assert
            act.ShouldThrow<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: otherTasks at index 1");
        }

        [Test]
        public void GivenTask_WhenAddingDependencyOfEmptyString_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn("SomeTask", "");

            //Act Assert
            act.ShouldThrow<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: otherTasks at index 1");
        }


        [Test]
        public void GivenTask_WhenAddingSameDependencyTwice_ThenDependencyExistExceptionThrown()
        {
            //Assign
            Sut.DependsOn("SomeName");

            //Act
            Action act = () => Sut.DependsOn("SomeName");

            //Assert
            act.ShouldThrow<DependencyExistException>();
        }
    }
}
