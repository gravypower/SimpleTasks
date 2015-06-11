using System.Collections.Generic;
using System.Linq;

namespace SimpleTasks.Tests.TaskContainer
{
    public class TaskContainerSpy : SimpleTasks.TaskContainer
    {
        public TaskContainerSpy(bool enforceDependencyOnAddOrder)
            : base(enforceDependencyOnAddOrder)
        {
        }
        public List<ITask> TasksSpy
        {
            get { return Tasks.ToList(); }
        }
    }
}
