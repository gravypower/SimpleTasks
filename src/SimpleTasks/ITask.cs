using System;

namespace SimpleTasks
{
    public interface ITask
    {
        Func<bool> Condition { get;} 

        string Name { get; }

        Action Action { get; }

        bool Invoked { get; }
    }
}
