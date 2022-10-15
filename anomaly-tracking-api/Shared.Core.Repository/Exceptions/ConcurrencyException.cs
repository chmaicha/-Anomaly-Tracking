using System;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace Shared.Core.Repository.Exceptions
{
    [Serializable]
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException()
        {
        }
        public ConcurrencyException(Exception exception) : base(exception.Message, exception)
        {
            this.MessageKey = "app.error.unexpected";
        }

        public ConcurrencyException(string message, string messageKey) : base(message)
        {
            this.MessageKey = messageKey;
        }

        public ConcurrencyException(string message) : base(message)
        {
            this.ErrorneousEntity = this.ParseExceptionMessage(message);
        }

        public ConcurrencyException(string messageKey, SqlException innerException) : base(innerException.Message, innerException)
        {
            this.MessageKey = messageKey;
            this.ErrorneousEntity = this.ParseExceptionMessage(innerException.Message);
        }

        protected ConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private string ParseExceptionMessage(string errorneousMessage)
        {
            if (!string.IsNullOrWhiteSpace(errorneousMessage) && errorneousMessage.Contains("UK"))
            {
                int? start = errorneousMessage?.IndexOf('(');
                int? end = errorneousMessage?.IndexOf(')');

                if (start >= 0 && end > 0 && start < end)
                {
                    ++start;
                    var length = end.Value - start.Value - 3;
                    return length > 0 ? errorneousMessage.Substring(start.Value, end.Value - start.Value - 3) : "";
                }
            }

            return string.Empty;
        }

        public string MessageKey { get; set; }

        public string ErrorneousEntity { get; set; }
    }
}