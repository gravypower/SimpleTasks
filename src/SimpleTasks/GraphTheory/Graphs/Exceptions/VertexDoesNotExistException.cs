using System;
using System.Runtime.Serialization;

namespace SimpleTasks.GraphTheory.Graphs.Exceptions
{
    [Serializable]
    public class VertexDoesNotExisException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public VertexDoesNotExisException()
        {
        }

        public VertexDoesNotExisException(string message) : base(message)
        {
        }

        public VertexDoesNotExisException(string message, Exception inner) : base(message, inner)
        {
        }

        protected VertexDoesNotExisException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}