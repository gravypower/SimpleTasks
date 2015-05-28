using System;
using System.Collections.Generic;
using System.Linq;
using SimpleTasks.GraphTheory.Graphs.Exceptions;

namespace SimpleTasks.GraphTheory.Graphs
{
    public class DirectedAcyclicGraph<TVertex> : Graph<TVertex>, IDirectedAcyclicGraph<TVertex>
    {
        public DirectedAcyclicGraph()
        {
            VerticesAndEdges = new Dictionary<TVertex, IList<IEdge<TVertex>>>();
        }

        public void InsertVertex(TVertex vertex)
        {
            GuardVertex(vertex);

            if (!VerticesAndEdges.ContainsKey(vertex))
                VerticesAndEdges.Add(vertex, new List<IEdge<TVertex>>());
        }

        public void InsertEdge(TVertex source, TVertex target)
        {
            GuardEdge(source, target);
            VerticesAndEdges[source].Add(new Edge<TVertex> {Source = source, Target = target});
        }

        private void GuardEdge(TVertex source, TVertex target)
        {
            if (source == null || target == null)
                throw new ArgumentNullException();

            if (ReferenceEquals(source, target))
                throw new InvalidEdgeException();

            if (!VerticesAndEdges.Any())
                throw new NoVerticesException();

            if (!VerticesAndEdges.ContainsKey(source) || !VerticesAndEdges.ContainsKey(target))
                throw new VertexDoesNotExisException();

            GuardForCycles(source, target);
        }

        private void GuardForCycles(TVertex source, TVertex target)
        {
           var targetVertex = VerticesAndEdges[target];

            foreach (var edge in targetVertex)
            {
                if(edge.Target.Equals(source))
                    throw new NonAcyclicGraphException();

                GuardForCycles(source, edge.Target);
            }
        }
    }
}
