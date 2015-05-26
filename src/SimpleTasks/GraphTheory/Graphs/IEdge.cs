namespace SimpleTasks.GraphTheory.Graphs
{
    public interface IEdge<TVertex>
    {
        TVertex Source { get; }
        TVertex Target { get; }
    }
}
