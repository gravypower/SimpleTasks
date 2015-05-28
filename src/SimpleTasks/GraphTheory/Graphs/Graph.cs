using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTasks.GraphTheory.Graphs
{
    public abstract class Graph<TVertex> : IGraph<TVertex>
    {
        public IDictionary<TVertex, IList<IEdge<TVertex>>> VerticesAndEdges { get; protected set; }

        protected abstract void GuardEdge(TVertex source, TVertex target);

        protected Graph()
        {
            VerticesAndEdges = new Dictionary<TVertex, IList<IEdge<TVertex>>>();
        }

        public void InsertVertex(TVertex vertex)
        {
            GuardVertex(vertex);

            if (!VerticesAndEdges.ContainsKey(vertex))
                VerticesAndEdges.Add(vertex, new List<IEdge<TVertex>>());
        }

        public virtual void InsertEdge(TVertex source, TVertex target)
        {
            GuardEdge(source, target);
            VerticesAndEdges[source].Add(new Edge<TVertex> { Source = source, Target = target });
        }

        public IEnumerable<TVertex> Vertices
        {
            get { return VerticesAndEdges.Keys.ToList().AsReadOnly(); }
        }

        public IEnumerable<IEdge<TVertex>> Edges
        {
            get { return VerticesAndEdges.Values.SelectMany(value => value).ToList().AsReadOnly(); }
        }

        protected static void GuardVertex(TVertex vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException();
        }
    }
}
