using Xunit;

namespace SimpleTasks.Tests.Pipelines
{
    public class PipelineTests
    {
        [Fact]
        public void Test()
        {
            var sut = new PipelineSpy();
        }
    }
}