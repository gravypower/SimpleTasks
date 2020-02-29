using FluentAssertions;
using SimpleTasks.GraphTheory.Graphs;
using System;
using SimpleTasks.GraphTheory.Graphs.Exceptions;
using Xunit;

namespace SimpleTasks.Tests.GraphTheory.Graphs
{
    public class DirectedAcyclicGraphTests
    {
        public DirectedAcyclicGraph<DummeyType> Cut;
        
        public DirectedAcyclicGraphTests()
        {
            Cut = new DirectedAcyclicGraph<DummeyType>();
        }

        [Fact]
        public void WhenInsertingNullVertex_ThenArgumentNullExceptionThrown()
        {
            //Arrange
            Action act = () => Cut.InsertVertex(null);

            //Act Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WhenInsertingVertex_ThenVertexIsInGraph()
        {
            //Arrange
            var vertex = new DummeyType();
            
            //Act
            Cut.InsertVertex(vertex);

            //Assert
            Cut.VerticesAndEdges.Should().ContainKey(vertex);
        }

        [Fact]
        public void WhenInsertingTwoVertices_ThenVerticesAreInGraph()
        {
            //Arrange
            var vertex = new DummeyType();
            var vertex2 = new DummeyType();

            //Act
            Cut.InsertVertex(vertex);
            Cut.InsertVertex(vertex2);

            //Assert
            Cut.VerticesAndEdges.Should().ContainKey(vertex);
            Cut.VerticesAndEdges.Should().ContainKey(vertex2);
        }

        [Fact]
        public void WhenInsertingNullSourceVertex_ThenArgumentNullExceptionThrown()
        {
            //Arrange
            Action act = () => Cut.InsertEdge(null, null);

            //Act Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GivenSourceVertex_WhenInsertingNullTargetVertex_ThenArgumentNullExceptionThrown()
        {
            //Arrange
            var source = new DummeyType();
            Action act = () => Cut.InsertEdge(source, null);

            //Act Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GivenTargetVertex_WhenInsertingNullSourceVertex_ThenArgumentNullExceptionThrown()
        {
            //Arrange
            var target = new DummeyType();
            Action act = () => Cut.InsertEdge(null, target);

            //Act Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GivenNoVertices_WhenInsertingAnEdge_ThenNoVerticesExceptionThrown()
        {
            //Arrange
            var source = new DummeyType();
            var target = new DummeyType();

            Action act = () => Cut.InsertEdge(source, target);

            //Act Assert
            act.Should().Throw<NoVerticesException>();
        }

        [Fact]
        public void GivenSourceVertex_WhenInsertingAnEdge_ThenVertexDoesNotExistExceptionThrown()
        {
            //Arrange
            var source = new DummeyType();
            Cut.InsertVertex(source);

            var target = new DummeyType();

            Action act = () => Cut.InsertEdge(source, target);

            //Act Assert
            act.Should().Throw<VertexDoesNotExisException>();
        }

        [Fact]
        public void GivenTargetVertex_WhenInsertingAnEdge_ThenVertexDoesNotExistExceptionThrown()
        {
            //Arrange
            var source = new DummeyType();
            var target = new DummeyType();
            Cut.InsertVertex(target);

            Action act = () => Cut.InsertEdge(source, target);

            //Act Assert
            act.Should().Throw<VertexDoesNotExisException>();
        }

        [Fact]
        public void GivenOneVertex_WhenInsertingAnEdgeToItsSelf_ThenInvalidEdgeThrown()
        {
            //Arrange
            var source = new DummeyType();
            Cut.InsertVertex(source);

            Action act = () => Cut.InsertEdge(source, source);

            //Act Assert
            act.Should().Throw<InvalidEdgeException>();
        }

        [Fact]
        public void GivenTwoVertices_WhenInsertingAnEdge_ThenNoExceptionThrown()
        {
            //Arrange
            var source = new DummeyType();
            Cut.InsertVertex(source);
            var target = new DummeyType();
            Cut.InsertVertex(target);

            Action act = () => Cut.InsertEdge(source, target);

            //Act Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void GivenTwoVertices_WhenAddingAnEdge_ThenEdgeAdded()
        {
            //Arrange
            var source = new DummeyType();
            Cut.InsertVertex(source);
            var target = new DummeyType();
            Cut.InsertVertex(target);

            //Act
            Cut.InsertEdge(source, target);

            //Assert
            Cut.VerticesAndEdges[source][0].Should().BeEquivalentTo(
                new Edge<DummeyType> {Source = source, Target = target});
        }

        [Fact]
        public void GivenTwoVertices_WhenAddingEdgeInBothDirections_ThenNonAcyclicGraphExceptionThrown()
        {
            //Arrange
            var source = new DummeyType();
            Cut.InsertVertex(source);
            var target = new DummeyType();
            Cut.InsertVertex(target);
            Cut.InsertEdge(source, target);
            Action act = () => Cut.InsertEdge(target, source);

            //Act Assert
            act.Should().Throw<NonAcyclicGraphException>();
        }

        [Fact]
        public void GivenTwoVertices_WhenAddingEdgeInBeforeSecound_ThenNoExceptionThrown()
        {
            //Arrange
            var source = new DummeyType();
            Cut.InsertVertex(source);
            var target = new DummeyType();
            Cut.InsertVertex(target);
            Cut.InsertEdge(source, target);
            Action act = () => Cut.InsertVertex(target);

            //Act Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void GivenThreeVertices_WhenAddingTwoEdges_NoExceptionThrown()
        {
            //Arrange
            var source = new DummeyType();
            Cut.InsertVertex(source);
            var target = new DummeyType();
            Cut.InsertVertex(target);

            var anotherTarget = new DummeyType();
            Cut.InsertVertex(anotherTarget);

            Cut.InsertEdge(source, target);
            Action act = () => Cut.InsertEdge(source, anotherTarget);

            //Act Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void GivenThreeVertices_WhenAddingCyclicEdges_ThenNonAcyclicGraphExceptionThrown()
        {
            //Arrange
            var vertexOne = new DummeyType("vertexOne");
            Cut.InsertVertex(vertexOne);

            var vertexTwo = new DummeyType("vertexTwo");
            Cut.InsertVertex(vertexTwo);

            var vertexThree = new DummeyType("vertexThree");
            Cut.InsertVertex(vertexThree);

            Cut.InsertEdge(vertexOne, vertexTwo);
            Cut.InsertEdge(vertexTwo, vertexThree);

            Action act = () => Cut.InsertEdge(vertexThree, vertexOne);

            //Act Assert
            act.Should().Throw<NonAcyclicGraphException>();
        }

        [Fact]
        public void GivenFourVertices_WhenAddingCyclicEdges_ThenNonAcyclicGraphExceptionThrown()
        {
            //Arrange
            var vertexOne = new DummeyType("vertexOne");
            Cut.InsertVertex(vertexOne);

            var vertexTwo = new DummeyType("vertexTwo");
            Cut.InsertVertex(vertexTwo);

            var vertexThree = new DummeyType("vertexThree");
            Cut.InsertVertex(vertexThree);

            var vertexFour = new DummeyType("vertexFour");
            Cut.InsertVertex(vertexFour);

            Cut.InsertEdge(vertexOne, vertexTwo);
            Cut.InsertEdge(vertexTwo, vertexThree);
            Cut.InsertEdge(vertexThree, vertexFour);

            Action act = () => Cut.InsertEdge(vertexFour, vertexOne);

            //Act Assert
            act.Should().Throw<NonAcyclicGraphException>();
        }


        [Fact]
        public void GivenFiveVertices_WhenAddingCyclicEdges_ThenNonAcyclicGraphExceptionThrown()
        {
            //Arrange
            var vertexOne = new DummeyType("vertexOne");
            Cut.InsertVertex(vertexOne);

            var vertexTwo = new DummeyType("vertexTwo");
            Cut.InsertVertex(vertexTwo);

            var vertexThree = new DummeyType("vertexThree");
            Cut.InsertVertex(vertexThree);

            var vertexFour = new DummeyType("vertexFour");
            Cut.InsertVertex(vertexFour);

            var vertexFive = new DummeyType("vertexFive");
            Cut.InsertVertex(vertexFive);

            Cut.InsertEdge(vertexOne, vertexTwo);
            Cut.InsertEdge(vertexTwo, vertexThree);
            Cut.InsertEdge(vertexThree, vertexFour);

            Action act = () => Cut.InsertEdge(vertexFour, vertexOne);

            //Act Assert
            act.Should().Throw<NonAcyclicGraphException>();
        }
    }
}
