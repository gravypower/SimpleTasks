using System;

namespace SimpleTasks
{
    public interface ITask
    {
        string Name { get; set; }

        Action Action { get; }

        bool Invoked { get; set; }

        ITask DependsOn(params string[] otherTasks);

        ITask DependsOn(params object[] otherTasks);

        ITask DependsOn(string name, Action action);

        ITask DependsOn(object task, Action action);
    }
}
