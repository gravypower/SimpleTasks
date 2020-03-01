using System.Linq;
using FluentAssertions;
using Xunit;

namespace SimpleTasks.Tests.TaskContainer
{
    public class ConditionalTaskTastsTests
    {
        public TaskContainerSpy Sut { get; set; }
        
        public ConditionalTaskTastsTests()
        {
            Sut = new TaskContainerSpy();
        }

        [Fact]
        public void GivenConditionalTask_WhenTaskIsRegistered_ThenTaskAddedHasCondition()
        {
            var callOrder = string.Empty;
            
            //Act
            Sut.Register("Task1", () => callOrder += "1", () => false);

            //Assert
            Sut.TasksSpy.First().Condition.Should().NotBeNull();
        }

        [Fact]
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
