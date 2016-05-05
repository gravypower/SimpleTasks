using System;
using System.Runtime.Serialization;

namespace SimpleTasks.Exceptions
{
    [Serializable]
    public class DependencyExistException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DependencyExistException()
        {
        }

        public DependencyExistException(string message)
            : base(message)
        {
        }

        public DependencyExistException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DependencyExistException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
