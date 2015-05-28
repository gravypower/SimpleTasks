using System.Collections.Generic;
using System.Linq;

namespace SimpleTasks.GraphTheory.Graphs
{
    public static class GraphExtensions
    {
        public static bool HasPredecessors<TVertex>(this IDictionary<TVertex, IList<IEdge<TVertex>>> verticesAndEdges, TVertex vertex)
        {
            return verticesAndEdges.Values.Any(el => el.Any(e => e.Target.Equals(vertex)));
        }
    }
}
