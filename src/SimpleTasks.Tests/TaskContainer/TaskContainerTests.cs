using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SimpleTasks.Exceptions;
using Xunit;

namespace SimpleTasks.Tests.TaskContainer
{
    public class TaskContainerTests
    {
        public TaskContainerSpy Sut { get; set; }
        
        public TaskContainerTests()
        {
            Sut = new TaskContainerSpy(false);
        }

        [Fact]
        public void GivenNullTask_WhenAddedToContainer_ThenNullIsGuarded()
        {
            //Assign
            const string taskName = "SomeName";
            Action act = () => Sut.Register(taskName, null);

            //Act Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'action')");
        }

        [Fact]
        public void GivenNullTaskName_WhenAddedToContainer_ThenNullIsGuarded()
        {
            //Assign
            Action act = () => Sut.Register(null, DoNothing);

            //Act Assert
            act.Should().Throw<ArgumentNullOrEmptyException>()
                .WithMessage("Value cannot be null or empty. (Parameter 'taskName')");
        }

        [Fact]
        public void GivenTask_WhenAddedToContainer_ThenNoExceptionIsThrown()
        {
            //Assign
            const string taskName = "SomeName";
            Action act = () => Sut.Register(taskName, DoNothing);

            //Act Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void GivenTask_WhenAddedToContainerTwice_ThenTaskExistsExceptionIsThrown()
        {
            //Assign
            const string taskName = "SomeName";
            Sut.Register(taskName, DoNothing);
            Action act = () => Sut.Register(taskName, DoNothing);

            //Act Assert
            act.Should().Throw<TaskExistsException>();
        }

        [Fact]
        public void GivenTask_WhenAddedToContainer_ThenItIsAddedToList()
        {
            //Act
            const string taskName = "SomeName";
            Sut.Register(taskName, DoNothing);

            //Assert
            Sut.TasksSpy.Should().NotBeEmpty();
            Sut.TasksSpy.Should().HaveCount(1);
        }

        [Fact]
        public void GivenTask_WhenAddedToContainer_ThenItIsAddedToList_AndNameIsCorrect()
        {
            //Assign
            const string taskName = "SomeName";

            //Act
            Sut.Register(taskName, DoNothing);
            var task = Sut.TasksSpy.First();
            task.Action.Invoke();

            //Assert
            task.Name.Should().Be(taskName);
        }

        [Fact]
        //This might need to be moved to the Task Tests
        public void GivenTaskAddedToContainer_WhenActionIsInvoked_ActionHasBeenInvoked()
        {
            //Assign
            var called = false;
            const string taskName = "SomeName";

            //Act
            Sut.Register(taskName, () => called = true);
            var task = Sut.TasksSpy.First();
            task.Action.Invoke();

            //Assert
            called.Should().BeTrue();
            task.Invoked.Should().BeTrue();
        }

        [Fact]
        public void GivenTaskAddedToContainer_WhenRunIsCalled_TaskIsRun()
        {
            //Assign
            var called = false;
            const string taskName = "SomeName";
            Sut.Register(taskName, () => called = true);

            //Act
            Sut.Run();

            //Assert
            called.Should().BeTrue();
        }

        [Fact]
        public void GivenTwoTasksAddedToContainer_WhenRunIsCalled_TaskIsRun()
        {
            //Assign
            var called1 = false;
            Sut.Register("SomeName", () => called1 = true);

            var called2 = false;
            Sut.Register("SomeOtherName", () => called2 = true);

            //Act
            Sut.Run();

            //Assert
            called1.Should().BeTrue();
            called2.Should().BeTrue();
        }

        [Fact]
        public void GivenOneTaskTheDependsOnATastThatDoesNotExist_WhereRUnCalled_DependantTaskDoesNotExistExceptionThrown()
        {
            //Assign
            Sut.Register("SomeName", DoNothing).DependsOn("SomeOtherName");

            Action act = () => Sut.Run();

            //Act Assert
            act.Should().Throw<DependicyDoesNotExistException>();
        }

        [Fact]
        public void Given2Tasks_1DependsOn2_WhenRunCalledOrderIs21()
        {
            //Assign
            var callOrder = string.Empty;

            Sut.Register("Task1", () => callOrder += "1").DependsOn("Task2");
            Sut.Register("Task2", () => callOrder += "2");

            //Act
            Sut.Run();

            //Assert
            callOrder.Should().Be("21");
        }

        [Fact]
        public void Given3Tasks_3DependsOn2And2DependsOn1_WhenRunCalledOrderIs123()
        {
            //Assign
            var callOrder = string.Empty;

            Sut.Register("Task1", () => callOrder += "1");
            Sut.Register("Task2", () => callOrder += "2").DependsOn("Task1");
            Sut.Register("Task3", () => callOrder += "3").DependsOn("Task2");

            //Act
            Sut.Run();

            //Assert
            callOrder.Should().Be("123");
        }

        [Fact]
        public void GivenTwoTasks_WhereTheTwoTasksHaveACircularDependency_CircularDependencyExceptionThrown()
        {
            //Assign
            Sut.Register("SomeName", DoNothing).DependsOn("SomeOtherName");

            Action act = () => Sut.Register("SomeOtherName", DoNothing).DependsOn("SomeName");

            //Act Assert
            act.Should().Throw<SimpleTasks.GraphTheory.Graphs.Exceptions.NonAcyclicGraphException>();
        }

        [Fact]
        public void GivenCustomTaskClass_WhenAddedToContainer_CustomClassAdded()
        {
            //Assign
            var customTask = new TaskSpy();

            //Act
            Sut.Register(customTask.Run);

            //Assert
            Sut.TasksSpy.Should().NotBeEmpty();
            Sut.TasksSpy.Should().HaveCount(1);
        }

        [Fact]
        public void GivenCustomTaskAddedToContainer_WhenRunIsCalled_TaskIsRun()
        {
            //Assign
            var customTask = new TaskSpy();
            Sut.Register(customTask.Run);

            //Act
            Sut.Run();

            //Assert
            customTask.Called.Should().BeTrue();
        }

        [Fact]
        public void GivenCustomTask_WhenAddedToContainerTwice_ThenTaskExistsExceptionIsThrown()
        {
            //Assign
            var customTask = new TaskSpy();
            Sut.Register(customTask.Run);
            Action act = () => Sut.Register(customTask.Run);

            //Act Assert
            act.Should().Throw<TaskExistsException>();
        }

        [Fact]
        public void GivenCustomTask_WhenTwoToContainer_ThenTaskExistsExceptionIsNOtThrown()
        {
            //Assign
            var customTask = new TaskSpy();
            var customTask2 = new TaskSpy();
            Sut.Register(customTask.Run);
            Action act = () => Sut.Register(customTask2.Run);

            //Act Assert
            act.Should().NotThrow<TaskExistsException>();
        }

        [Fact]
        public void GivenOneCustomTaskTheDependsOnATastThatDoesNotExist_WhereRunCalled_DependantTaskDoesNotExistExceptionThrown()
        {
            //Assign
            var customTask = new TaskSpy();
            Sut.Register(customTask.Run).DependsOn("SomeOtherName");

            Action act = () => Sut.Run();

            //Act Assert
            act.Should().Throw<DependicyDoesNotExistException>();
        }

        [Fact]
        public void GivenOneCustomTaskTheDependsOnACustomTastThatDoesNotExist_WhereRunCalled_DependantTaskDoesNotExistExceptionThrown()
        {
            //Assign
            var customTask = new TaskSpy();
            var customTask2 = new TaskSpy();
            Sut.Register(customTask.Run).DependsOn(customTask2);

            Action act = () => Sut.Run();

            //Act Assert
            act.Should().Throw<DependicyDoesNotExistException>();
        }

        [Fact]
        public void Given2CustomTasks_1DependsOn2_WhenRunCalledOrderIs21()
        {
            //Assign
            var callOrder = new List<string>();

            var customTask = new TaskSpy(callOrder, "1");
            var customTask2 = new TaskSpy(callOrder, "2");

            Sut.Register(customTask.Run).DependsOn(customTask2);
            Sut.Register(customTask2.Run);

            //Act
            Sut.Run();

            //Assert
            string.Join("", callOrder).Should().Be("21");
        }

        [Fact]
        public void Given3CustomTasks_3DependsOn2And2DependsOn1_WhenRunCalledOrderIs123()
        {
            //Assign
            var callOrder = new List<string>();

            var customTask = new TaskSpy(callOrder, "1");
            var customTask2 = new TaskSpy(callOrder,"2");
            var customTask3 = new TaskSpy(callOrder,"3");

            Sut.Register(customTask.Run);
            Sut.Register(customTask2.Run).DependsOn(customTask);
            Sut.Register(customTask3.Run).DependsOn(customTask2);

            //Act
            Sut.Run();

            //Assert
            string.Join("", callOrder).Should().Be("123");
        }

        [Fact]
        public void Given3CustomTasks_AddOrderDependicy_WhenRunCalledOrderIs123()
        {
            //Assign
            Sut = new TaskContainerSpy(true);

            var callOrder = new List<string>();

            var customTask = new TaskSpy(callOrder, "1");
            var customTask2 = new TaskSpy(callOrder, "2");
            var customTask3 = new TaskSpy(callOrder, "3");

            Sut.Register(customTask.Run);
            Sut.Register(customTask3.Run);
            Sut.Register(customTask2.Run);

            //Act
            Sut.Run();

            //Assert
            string.Join("", callOrder).Should().Be("132");
        }

        [Fact]
        public void GivenCustomTwoTasks_WhereTheTwoTasksHaveACircularDependency_CircularDependencyExceptionThrown()
        {
            //Assign
            var customTask = new TaskSpy();
            var customTask2 = new TaskSpy();
            Sut.Register(customTask.Run).DependsOn(customTask2);

            Action act = () => Sut.Register(customTask2.Run).DependsOn(customTask);

            //Act Assert
            act.Should().Throw<SimpleTasks.GraphTheory.Graphs.Exceptions.NonAcyclicGraphException>();
        }

        [Fact]
        public void GivenTwoTasksOneNamed1AndTwoAnObject_DependicyNamesDoNotCollide()
        {
            //Assign
            var callOrder = new List<string>();

            var customTask = Sut.Register("1", () => callOrder.Add("1"));
            var customTask2 = new TaskSpy(callOrder, "2");
            Sut.Register(customTask2.Run);
            customTask.DependsOn(customTask2);
            
            //Act
            Sut.Run();

            //Assert
            string.Join("", callOrder).Should().Be("21");
        }


        public void DoNothing()
        {
        }
    }
}
