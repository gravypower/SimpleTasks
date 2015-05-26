using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace SimpleTasks.Tests.TaskContainer
{
    [TestFixture]
    public class TaskContainerAPITests
    {
        public TaskContainerSpy Sut { get; set; }

        [SetUp]
        public void SetUp()
        {
            Sut = new TaskContainerSpy(false);
        }

        [Test]
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

        [Test]
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

        [Test]
        public void GivenTwoTasksOneNamed1AndTwoAnObject_CanDeclareTaskAsDependency()
        {
            //Assign
            var callOrder = new List<string>();

            var customTask = Sut.Register("1", () => callOrder.Add("1"));
            var customTask2 = new TaskSpy(callOrder, "2");
            customTask.DependsOn(customTask2, customTask2.Run);

            //Act
            Sut.Run();

            //Assert
            string.Join("", callOrder).Should().Be("21");
        }
    }
}
