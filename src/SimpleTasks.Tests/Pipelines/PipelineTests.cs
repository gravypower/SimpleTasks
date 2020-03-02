using System;
using System.Collections.Generic;
using FluentAssertions;
using SimpleTasks.Tests.Pipelines.Nodes;
using Xunit;

namespace SimpleTasks.Tests.Pipelines
{
    public class PipelineTests
    {
        [Fact]
        public void GivenPipelineWith3Nodes_WithNoDependencies_ShouldNotThrow()
        {
            //Arrange
            var callOrder = new List<string>();
            var sut = new PipelineSpy(
                callOrder,
                new DummyNode[]
            {
                new DummyNodeTwo(), 
                new DummyNodeOne(), 
                new DummyNodeThree() 
            });
            
            //Act
            Action act = () => sut.Run();
            
            //Assert
            act.Should().NotThrow();
        }
        
        [Fact]
        public void GivenPipelineWith3Nodes_WhereOneDependsOnThree_OrderSholdBeCorrect()
        {
            //Arrange
            var callOrder = new List<string>();
            var sut = new PipelineSpy(
                callOrder,
                new DummyNode[]
            {
                new DummyNodeDependsOnThree(), 
                new DummyNodeOne(),
                new DummyNodeThree() 
            });
            
            //Act
            sut.Run();
            
            //Assert
            var dummyNodeThreeOrder = callOrder.IndexOf(typeof(DummyNodeThree).AssemblyQualifiedName);
            var dummyNodeDependsOnThreeOrder = callOrder.IndexOf(typeof(DummyNodeDependsOnThree).AssemblyQualifiedName);
            dummyNodeThreeOrder.Should().BeLessThan(dummyNodeDependsOnThreeOrder);

        }
    }
}