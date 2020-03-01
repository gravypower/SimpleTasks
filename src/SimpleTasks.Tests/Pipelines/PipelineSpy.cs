using SimpleTasks.Pipelines;

namespace SimpleTasks.Tests.Pipelines
{
    public class PipelineSpy:Pipeline<Node<DummyInput, DummyContext>, DummyInput, DummyContext>
    {
        protected override void DoRun(string vertexName)
        {
            throw new System.NotImplementedException();
        }
    }
}