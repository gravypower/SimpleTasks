using FluentAssertions;
using Xunit;

namespace SimpleTasks.Tests.TaskContainer
{
    public class TaskContainerApiTests
    {
        public TaskContainerSpy Sut { get; set; }

        public TaskContainerApiTests()
        {
            Sut = new TaskContainerSpy();
        }

        [Fact]
        public void CanChainDependencies()
        {
            //Assign
            var callOrder = string.Empty;

            Sut.Register("Task1", () => callOrder += "1").DependsOn("Task2").DependsOn("Task3");
            Sut.Register("Task2", () => callOrder += "2");
            Sut.Register("Task3", () => callOrder += "3");

            //Act
            Sut.Run();

            //Assert
            callOrder.Should().Be("231");
        }

        [Fact]
        public void CanDeclareTaskAsDependency()
        {
            //Assign
            var callOrder = string.Empty;

            Sut.Register("Task1", () => callOrder += "1")
                .DependsOn("Task2")
                .DependsOn("Task3", () => callOrder += "3");

            Sut.Register("Task2", () => callOrder += "2");

            //Act
            Sut.Run();

            //Assert
            callOrder.Should().Be("231");
        }
    }
}
