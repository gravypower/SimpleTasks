using SimpleTasks.Tasks;

namespace SimpleTasks.Pipelines
{
    public abstract class Pipeline<TNode, TInput, TContext>: VertexContainer
        where  TNode : Node<TInput, TContext>
    {
       
    }
}