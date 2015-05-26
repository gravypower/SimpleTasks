using System.Collections.Generic;
using SimpleTasks.GraphTheory.Graphs;

namespace SimpleTasks.GraphTheory.Algorithms
{
    public static class AlgorithmExtensions
    {
        public static IList<TVertex> TopologicalSort<TVertex>(this DirectedAcyclicGraph<TVertex> graph)
        {
            var topologicalSortAlgorithm = new TopologicalSortAlgorithm<TVertex>(graph);
            topologicalSortAlgorithm.Compute();
            return topologicalSortAlgorithm.SortedVertices;
        }
    }
}
