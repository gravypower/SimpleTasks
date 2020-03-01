using System;

namespace SimpleTasks.Pipelines.Attributes
{
    public class DependsOnAttribute:Attribute 
    {
        private readonly Type _type;

        public DependsOnAttribute(Type type)
        {
            _type = type;
        }
    }
}