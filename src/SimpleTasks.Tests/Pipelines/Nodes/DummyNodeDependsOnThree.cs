using SimpleTasks.Pipelines.Attributes;

namespace SimpleTasks.Tests.Pipelines.Nodes
{
    [DependsOn(typeof(DummyNodeThree))]
    public class DummyNodeDependsOnThree:DummyNode
    {
    }
}