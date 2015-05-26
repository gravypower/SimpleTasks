using System;
using System.Runtime.Serialization;

namespace SimpleTasks.Exceptions
{
    [Serializable]
    public class DependicyDoesNotExistException : Exception
    {

        public DependicyDoesNotExistException()
        {
        }

        public DependicyDoesNotExistException(string message)
            : base(message)
        {
        }

        public DependicyDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DependicyDoesNotExistException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
