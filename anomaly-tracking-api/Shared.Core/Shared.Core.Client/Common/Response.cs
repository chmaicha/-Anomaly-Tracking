using System;
using System.IO;
using System.Runtime.Serialization;

namespace Shared.Core.Client.Common
{
    /// <summary>
    /// Represensts the <code>Response</code> object instance.
    /// </summary>
    /// <typeparam name="TEntity">Data type</typeparam>
    [DataContract]
    public class Response<TEntity>
    {
        public Response() { }

        public Response(TEntity data, string messageKey, int? totalCount = 0)
        {
            this.Data = data;
            this.MessageKey = messageKey;
            this.IsSuccessful = true;
            this.TotalCount = totalCount;
        }

        public Response(Exception exception, string messageKey, string errorneousEntity = "")
        {
            this.IsSuccessful = false;
            this.MessageKey = messageKey;
            this.ErrorneousEntity = errorneousEntity;

            if (this.Exception != null)
            {
                this.Message = exception.Message;
                this.Exception = exception;
                this.BuildInnerExceptions(exception);
            }
        }

        public Response(ArgumentException exception)
            : this(exception, exception.Message)
        {
        }

        public Response(ArgumentException exception, int statusCode)
            : this(exception, exception.Message)
        {
            this.StatusCode = statusCode;
        }

        public Response(IOException exception)
           : this(exception, exception.Message)
        {
        }

        public static Response<TEntity> Build<KEntity>(Response<KEntity> response)
        {
            return new Response<TEntity>
            {
                IsSuccessful = false,
                Message = response.MessageKey,
                MessageKey = response.MessageKey,
                Exception = response.Exception,
                ErrorneousEntity = response.ErrorneousEntity,
                InnerException = response.InnerException,
                StatusCode = response.StatusCode
            };
        }

        [DataMember]
        public TEntity Data { get; set; }

        [DataMember]
        public bool IsSuccessful { get; set; }

        [DataMember]
        public string Message { get; set; }

        public Exception Exception { get; private set; }

        [DataMember]
        public string InnerException { get; set; }

        [DataMember]
        public int? TotalCount { get; set; }

        [DataMember]
        public string MessageKey { get; set; }

        [DataMember]
        public string ErrorneousEntity { get; set; }

        [DataMember]
        public int StatusCode { get; private set; }

        private void BuildInnerExceptions(Exception exception)
        {
            if (exception?.InnerException == null)
            {
                return;
            }

            this.InnerException = $"{exception.InnerException.Message } -> {  this.InnerException }";

            this.BuildInnerExceptions(exception.InnerException);
        }
    }
}