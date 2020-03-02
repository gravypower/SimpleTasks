using System.Collections.Generic;
using SimpleTasks.Pipelines;

namespace SimpleTasks.Tests.Pipelines
{
    public class PipelineSpy:Pipeline<Node<DummyInput, DummyContext>, DummyInput, DummyContext>
    {
        private readonly List<string> _callOrder;

        public PipelineSpy(List<string> callOrder, IEnumerable<Node<DummyInput, DummyContext>> nodes) : base(nodes, PipelineContainerConfiguration.Default)
        {
            _callOrder = callOrder;
        }
        
        protected override void DoRun(string vertexName)
        {
            _callOrder.Add(vertexName);
        }
    }
}