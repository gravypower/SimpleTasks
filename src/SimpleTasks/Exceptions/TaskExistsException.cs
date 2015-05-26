using System;
using System.Runtime.Serialization;

namespace SimpleTasks.Exceptions
{
    [Serializable]
    public class TaskExistsException : Exception
    {
        public TaskExistsException()
        {
        }

        public TaskExistsException(string message)
            : base(message)
        {
        }

        public TaskExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected TaskExistsException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
