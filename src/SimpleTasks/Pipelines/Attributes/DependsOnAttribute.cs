using System;

namespace SimpleTasks.Pipelines.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependsOnAttribute:Attribute 
    {
        public Type Dependency { get; }

        public DependsOnAttribute(Type dependency)
        {
            Dependency = dependency;
        }
    }
}