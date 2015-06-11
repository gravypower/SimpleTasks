using System;

namespace SimpleTasks
{
    public interface ITask
    {
        Func<bool> Condition { get;} 

        string Name { get; }

        Action Action { get; }

        bool Invoked { get; }

        ITask DependsOn(params string[] otherTasks);

        ITask DependsOn(params object[] otherTasks);

        ITask DependsOn(string name, Action action);

        ITask DependsOn(object task, Action action);
    }
}
