using System;
using System.Runtime.Serialization;

namespace SimpleTasks.GraphTheory.Graphs.Exceptions
{
    [Serializable]
    public class InvalidEdgeException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidEdgeException()
        {
        }

        public InvalidEdgeException(string message) : base(message)
        {
        }

        public InvalidEdgeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidEdgeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
