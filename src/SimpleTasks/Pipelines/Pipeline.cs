namespace SimpleTasks.Pipelines
{
    public abstract class Pipeline<TNode, TInput, TContext>: VertexContainer<TNode>
        where  TNode : Node<TInput, TContext>
    {
       
    }
}