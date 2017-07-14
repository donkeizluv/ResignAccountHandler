using System;

namespace ResignAccountHandlerUI.Exceptions
{
    [Serializable]
    public class DbException : Exception
    {
        public DbException()
        {
        }

        public DbException(string message) : base(message)
        {
        }

        public DbException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DbException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}