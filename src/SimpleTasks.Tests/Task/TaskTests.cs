using System;
using FluentAssertions;
using SimpleTasks.Exceptions;
using SimpleTasks.Tasks;
using Xunit;

namespace SimpleTasks.Tests.Task
{
    public class TaskTests
    {
        public Tasks.Task Sut;
        
        public TaskTests()
        {
            var taskContainer = new Tasks.TaskContainer(TaskContainerConfiguration.Default);
            Sut = taskContainer.Register("SomeTask",  () => GetType());
        }

        [Fact]
        public void GivenTask_WhenAddingDependencyOfNull_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn(null);

            //Act Assert
            act.Should().Throw<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty. (Parameter 'otherTasks')");
        }

        [Fact]
        public void GivenTask_WhenAddingDependencyOfEmpty_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn(string.Empty);

            //Act Assert
            act.Should().Throw<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty. (Parameter 'otherTasks at index 0')");
        }

        [Fact]
        public void GivenTask_WhenAddingDependencyOfDefaultString_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn("SomeTask", default(string));

            //Act Assert
            act.Should().Throw<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty. (Parameter 'otherTasks at index 1')");
        }

        [Fact]
        public void GivenTask_WhenAddingDependencyOfEmptyString_ArgumentNullOrEmptyExceptionThrown()
        {
            //Assign
            Action act = () => Sut.DependsOn("SomeTask", "");

            //Act Assert
            act.Should().Throw<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty. (Parameter 'otherTasks at index 1')");
        }


        [Fact]
        public void GivenTask_WhenAddingSameDependencyTwice_ThenDependencyExistExceptionThrown()
        {
            //Assign
            Sut.DependsOn("SomeName");

            //Act
            Action act = () => Sut.DependsOn("SomeName");

            //Assert
            act.Should().Throw<DependencyExistException>();
        }
    }
}
