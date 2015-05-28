namespace SimpleTasks.GraphTheory.Graphs
{
    public interface IGraph<TVertex>
    {
        bool HasPredecessors(TVertex vertex);
    }
}