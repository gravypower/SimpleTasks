using System;
using System.Collections.Generic;
using System.Linq;
using SimpleTasks.GraphTheory.Graphs.Exceptions;

namespace SimpleTasks.GraphTheory.Graphs
{
    public class DirectedAcyclicGraph<TVertex> : IDirectedAcyclicGraph<TVertex>
    {
        public IDictionary<TVertex, IList<IEdge<TVertex>>> VerticesAndEdges { get; private set; }

        public IEnumerable<TVertex> Vertices
        {
            get { return VerticesAndEdges.Keys.ToList(); }
        }

        public IEnumerable<IEdge<TVertex>> Edges
        {
            get { return VerticesAndEdges.Values.SelectMany(value => value); }
        }

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

        private static void GuardVertex(TVertex vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException();
        }

        private void GuardEdge(TVertex source, TVertex target)
        {
            if (source == null || target == null)
                throw new ArgumentNullException();

            if (object.ReferenceEquals(source, target))
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
