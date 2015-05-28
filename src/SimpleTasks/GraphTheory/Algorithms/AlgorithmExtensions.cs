using System.Collections.Generic;
using SimpleTasks.GraphTheory.Graphs;

namespace SimpleTasks.GraphTheory.Algorithms
{
    public static class AlgorithmExtensions
    {
        public static IList<TVertex> TopologicalSort<TVertex>(this DirectedAcyclicGraph<TVertex> graph)
        {
            return new TopologicalSortAlgorithm<TVertex>(graph).SortedVertices;
        }
    }
}
