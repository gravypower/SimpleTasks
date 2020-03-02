namespace SimpleTasks.Pipelines
{
    public abstract class Node<TInput, TContext> : Vertex<Node<TInput, TContext>, Pipeline<Node<TInput, TContext>,TInput, TContext>>
    {
        protected Node()
        {
            Name = GetType().AssemblyQualifiedName;
        }
    }
}