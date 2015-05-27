using System.Collections.Generic;
using System.Linq;
using SimpleTasks.GraphTheory.Graphs;

namespace SimpleTasks.GraphTheory.Algorithms
{
    public class TopologicalSortAlgorithm<TVertex>
    {
        private readonly IDirectedAcyclicGraph<TVertex> _graph;

        private readonly List<TVertex> _sortedVertices;

        public IList<TVertex> SortedVertices
        {
            get { return _sortedVertices.AsReadOnly(); }
        }

        public TopologicalSortAlgorithm(IDirectedAcyclicGraph<TVertex> graph)
        {
            _graph = graph;
            _sortedVertices = new List<TVertex>();
            Compute();
        }

        private void Compute()
        {
            var workingList = new List<TVertex>(_graph.VerticesAndEdges.Keys);
            var workingGraph = _graph.VerticesAndEdges.ToDictionary(entry => entry.Key, entry => entry.Value);

            while (workingList.Any())
            {
                foreach (var vertex in workingList.ToArray())
                {
                    if (!HasPredecessors(vertex, workingGraph) || !HasEdge(workingGraph))
                    {
                        AddSortedVertex(vertex, workingList, workingGraph);
                    }
                }

                if (workingList.Count == 1)
                {
                    AddSortedVertex(workingList.Single(), workingList, workingGraph);
                }
            }
        }

        internal static bool HasPredecessors(TVertex vertex, IDictionary<TVertex, IList<IEdge<TVertex>>> verticesAndEdges)
        {
            return verticesAndEdges.Values.Any(el => el.Any(e => e.Target.Equals(vertex)));
        }

        private void AddSortedVertex(TVertex vertex, ICollection<TVertex> workingList,
            IDictionary<TVertex, IList<IEdge<TVertex>>> verticesAndEdges)
        {
            _sortedVertices.Add(vertex);
            workingList.Remove(vertex);
            verticesAndEdges[vertex].Clear();
        }

        private static bool HasEdge(IDictionary<TVertex, IList<IEdge<TVertex>>> verticesAndEdges)
        {
            return verticesAndEdges.Values.Any(el => el.Any());
        }
    }
}
