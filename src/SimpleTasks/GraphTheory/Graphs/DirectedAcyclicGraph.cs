using System;
using System.Linq;
using SimpleTasks.GraphTheory.Graphs.Exceptions;

namespace SimpleTasks.GraphTheory.Graphs
{
    public class DirectedAcyclicGraph<TVertex> : Graph<TVertex>
    {
        protected override void GuardEdge(TVertex source, TVertex target)
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
