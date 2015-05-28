using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTasks.GraphTheory.Graphs
{
    public abstract class Graph<TVertex> : IGraph<TVertex>
    {
        public IDictionary<TVertex, IList<IEdge<TVertex>>> VerticesAndEdges { get; protected set; }

        public IEnumerable<TVertex> Vertices
        {
            get { return VerticesAndEdges.Keys.ToList().AsReadOnly(); }
        }

        public IEnumerable<IEdge<TVertex>> Edges
        {
            get { return VerticesAndEdges.Values.SelectMany(value => value).ToList().AsReadOnly(); }
        }

        public bool HasPredecessors(TVertex vertex)
        {
            return VerticesAndEdges.Values.Any(el => el.Any(e => e.Target.Equals(vertex)));
        }

        protected static void GuardVertex(TVertex vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException();
        }
    }
}
