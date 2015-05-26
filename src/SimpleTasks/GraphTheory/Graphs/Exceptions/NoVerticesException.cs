using System;
using System.Runtime.Serialization;

namespace SimpleTasks.GraphTheory.Graphs.Exceptions
{
    [Serializable]
    public class NoVerticesException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public NoVerticesException()
        {
        }

        public NoVerticesException(string message) : base(message)
        {
        }

        public NoVerticesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoVerticesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
