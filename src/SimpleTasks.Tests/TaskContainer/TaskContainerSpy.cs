using System.Collections.Generic;
using System.Linq;
using SimpleTasks.Tasks;

namespace SimpleTasks.Tests.TaskContainer
{
    public class TaskContainerSpy : global::SimpleTasks.Tasks.TaskContainer
    {
        public TaskContainerSpy()
            : base(TaskContainerConfiguration.Default)
        {
        }
        public List<Tasks.Task> TasksSpy => Tasks.ToList();
    }
}
