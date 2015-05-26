using System;

namespace SimpleTasks.Tests.Task
{
    public class DummyTask:ITask
    {
        public string Name
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Action Action
        {
            get { throw new NotImplementedException(); }
        }

        public bool Invoked
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ITask DependsOn(params string[] otherTasks)
        {
            throw new NotImplementedException();
        }

        public ITask DependsOn(params object[] otherTasks)
        {
            throw new NotImplementedException();
        }

        public ITask DependsOn(string name, Action action)
        {
            throw new NotImplementedException();
        }

        public ITask DependsOn(object task, Action action)
        {
            throw new NotImplementedException();
        }
    }
}
