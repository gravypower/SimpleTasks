using System.Collections.Generic;
using System.Linq;

namespace SimpleTasks.Tests.TaskContainer
{
    public class TaskContainerSpy : global::SimpleTasks.TaskContainer
    {
        public TaskContainerSpy(bool enforceDependencyOnAddOrder)
            : base(enforceDependencyOnAddOrder)
        {
        }
        public List<SimpleTasks.Task> TasksSpy => Tasks.ToList();
    }
}
