using System;
using System.Runtime.Serialization;

namespace SimpleTasks.Exceptions
{
    [Serializable]
    public class DependencyDoesNotExistException : Exception
    {

        public DependencyDoesNotExistException()
        {
        }

        public DependencyDoesNotExistException(string message)
            : base(message)
        {
        }

        public DependencyDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DependencyDoesNotExistException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
