using System;

namespace SimpleTasks
{
    public interface ITaskContainer<out TTask>
    {
        void Run();

        TTask Register(string taskName, Action action, Func<bool> condition = null);

        void RegisterEmptyDependicy(string taskName);

        bool DoesContainTask(string taskName);

        void RegisterDependicy(string source, string target);

        bool DoesDependicyExist(string source, string target);
    }
}
