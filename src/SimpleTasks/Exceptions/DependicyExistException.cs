using System;
using System.Runtime.Serialization;

namespace SimpleTasks.Exceptions
{
    [Serializable]
    public class DependicyExistException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DependicyExistException()
        {
        }

        public DependicyExistException(string message)
            : base(message)
        {
        }

        public DependicyExistException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DependicyExistException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
