using System;

namespace SimpleTasks.Exceptions
{
    public class ArgumentNullOrEmptyException : ArgumentException
    {
        public ArgumentNullOrEmptyException(String paramName) 
            : base("Value cannot be null or empty.", paramName) {
        }
    }
}
