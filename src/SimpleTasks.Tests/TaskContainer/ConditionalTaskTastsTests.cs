using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace SimpleTasks.Tests.TaskContainer
{
    [TestFixture]
    public class ConditionalTaskTastsTests
    {
        public TaskContainerSpy Sut { get; set; }

        [SetUp]
        public void SetUp()
        {
            Sut = new TaskContainerSpy(false);
        }

        [Test]
        public void GivenConditionalTask_WhenTaskIsRegistered_ThenTaskAddedHasCondition()
        {
            var callOrder = string.Empty;
            
            //Act
            Sut.Register("Task1", () => callOrder += "1", () => false);

            //Assert
            Sut.TasksSpy.First().Condition.Should().NotBeNull();
        }

        [Test]
        public void GivenConditionalOfFalseTask_WhenTasksAreExcuted_TaskIsNotCalled()
        {

            var callOrder = string.Empty;

            //Arrange
            Sut.Register("Task1", () => callOrder += "1", () => false);
            
            //Act
            Sut.Run();

            //Assert
            callOrder.Should().BeEmpty();
        }
    }
}
