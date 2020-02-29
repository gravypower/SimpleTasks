using System;
using System.Diagnostics;
using FluentAssertions;
using SimpleTasks.GraphTheory.Algorithms;
using SimpleTasks.GraphTheory.Graphs;
using Xunit;

namespace SimpleTasks.Tests.GraphTheory.Algorithms
{
    public class TopologicalSortAlgorithmTests
    {
        public TopologicalSortAlgorithm<string> Cut;
        public IGraph<string> Graph;

        public TopologicalSortAlgorithmTests()
        {
            Graph = new DirectedAcyclicGraph<string>();
        }

        [Fact]
        public void GivenOneVertex_WhenComputeCalled_SortedVerticesContainsThatVertex()
        {
            //Assign Act
            Graph.InsertVertex("A");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            Cut.SortedVertices.Should().Contain("A");
        }

        [Fact]
        public void GivenTwoVertices_WhenComputeCalled_SortedVerticesContainsThoesVertices()
        {
            //Assign Act
            Graph.InsertVertex("A");
            Graph.InsertVertex("B");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            Cut.SortedVertices.Should().Contain("A");
            Cut.SortedVertices.Should().Contain("B");
        }

        [Theory]
        [InlineData("A", "B", "AB")]
        [InlineData("B", "A", "BA")]
        public void GivenTwoVertices_WhenComputeCalled_SortedVerticesShouldBeInCorrectOrder(
            string firstVertex,
            string secoundVertex, 
            string sortedVertices)
        {
            //Assign Act
            Graph.InsertVertex(firstVertex);
            Graph.InsertVertex(secoundVertex);
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            AssertOrder(sortedVertices);
        }

        [Fact]
        public void GivenTwoVertices_ThatHaveAnEdge__WhenComputeCalled_SortedVerticesShouldBeInCorrectOrder()
        {
            //Assign Act
            Graph.InsertVertex("A");
            Graph.InsertVertex("B");
            Graph.InsertEdge("A", "B");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            AssertOrder("AB");
        }

        [Fact]
        public void GivenTwoVerticesAddedInReverse_ThatHaveAnEdge__WhenComputeCalled_SortedVerticesShouldBeInCorrectOrder()
        {
            //Assign Act
            Graph.InsertVertex("B");
            Graph.InsertVertex("A");
            Graph.InsertEdge("A", "B");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            AssertOrder("AB");
        }

        [Fact]
        public void GivenTwoVerticesAndEdge__WhenHasPredecessorsCalledOnA_ShouldBeFalse()
        {
            //Assign
            Graph.InsertVertex("B");
            Graph.InsertVertex("A");
            Graph.InsertEdge("A", "B");

            //Act
            var result = Graph.VerticesAndEdges.HasPredecessors("A");

            //Assert
            result.Should().BeFalse();
        }


        [Fact]
        public void GivenThreeVerticesAndEdge__WhenHasPredecessorsCalledOnA_ShouldBeFalse()
        {
            //Assign
            Graph.InsertVertex("B");
            Graph.InsertVertex("A");
            Graph.InsertVertex("C");
            Graph.InsertEdge("A", "B");
            Graph.InsertEdge("B", "C");

            //Act
            var result = Graph.VerticesAndEdges.HasPredecessors("A");

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenThreeVerticesAndEdge__WhenHasPredecessorsCalledOnB_ShouldBeFalse()
        {
            //Assign
            Graph.InsertVertex("B");
            Graph.InsertVertex("A");
            Graph.InsertVertex("C");
            Graph.InsertEdge("A", "B");
            Graph.InsertEdge("B", "C");

            //Act
            var result = Graph.VerticesAndEdges.HasPredecessors("B");

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GivenThreeVerticesBAC_ThatHaveAnEdgeABBC__WhenComputeCalled_SortedVerticesShouldBeSourted()
        {
            //Assign Act
            Graph.InsertVertex("B");
            Graph.InsertVertex("A");
            Graph.InsertVertex("C");
            Graph.InsertEdge("A", "B");
            Graph.InsertEdge("B", "C");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            AssertOrder("ABC");
        }

        [Fact]
        public void GivenThreeVerticesCAB_ThatHaveAnEdgeABBC__WhenComputeCalled_SortedVerticesShouldBeSourted()
        {
            //Assign Act
            Graph.InsertVertex("C");
            Graph.InsertVertex("A");
            Graph.InsertVertex("B");
            Graph.InsertEdge("A", "B");
            Graph.InsertEdge("B", "C");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            AssertOrder("ABC");
        }

        [Fact]
        public void GivenFourVerticesCABD_ThatHaveAnEdgeABBCCD__WhenComputeCalled_SortedVerticesShouldBeSourted()
        {
            //Assign Act
            Graph.InsertVertex("C");
            Graph.InsertVertex("A");
            Graph.InsertVertex("B");
            Graph.InsertVertex("D");
            Graph.InsertEdge("A", "B");
            Graph.InsertEdge("B", "C");
            Graph.InsertEdge("C", "D");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            AssertOrder("ABCD");
        }

        [Fact]
        public void GivenFourVerticesCABD_ThatHaveAnEdgeABACCD__WhenComputeCalled_SortedVerticesShouldBeSourted()
        {
            //Assign Act
            Graph.InsertVertex("C");
            Graph.InsertVertex("A");
            Graph.InsertVertex("B");
            Graph.InsertVertex("D");
            Graph.InsertEdge("A", "B");
            Graph.InsertEdge("A", "C");
            Graph.InsertEdge("C", "D");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            AssertOrder("ABCD");
        }

        [Fact]
        public void GivenFourVerticesCABD_ThatHaveAnEdgeACABAD__WhenComputeCalled_SortedVerticesShouldBeSourted()
        {
            //Assign Act
            Graph.InsertVertex("C");
            Graph.InsertVertex("A");
            Graph.InsertVertex("B");
            Graph.InsertVertex("D");
            Graph.InsertEdge("A", "C");
            Graph.InsertEdge("A", "B");
            Graph.InsertEdge("A", "D");
            Cut = new TopologicalSortAlgorithm<string>(Graph);

            //Assert
            AssertOrder("ABDC");
        }

        [Fact]
        public void Intergreation()
        {
            //Assign Act
            Graph.InsertVertex("1");
            Graph.InsertVertex("2");
            Graph.InsertVertex("4");
            Graph.InsertVertex("6");
            Graph.InsertVertex("7");
            Graph.InsertVertex("9");

            Graph.InsertEdge("9", "2");
            Graph.InsertEdge("2", "7");
            Graph.InsertEdge("7", "4");
            Graph.InsertEdge("4", "1");
            Graph.InsertEdge("6", "1");

            var s = Stopwatch.StartNew();
            Cut = new TopologicalSortAlgorithm<string>(Graph);
            s.Stop();
            
            Console.WriteLine("Elapsed Time: {0} ms", s.ElapsedMilliseconds);

            //Assert
            AssertOrder("692741", "962741");
        }

        private void AssertOrder(params string[] sortedVerticesList)
        {
            string.Join("", Cut.SortedVertices).Should().BeOneOf(sortedVerticesList);
        }
    }
}
