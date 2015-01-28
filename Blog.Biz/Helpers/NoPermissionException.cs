using System;
using System.Runtime.Serialization;
using Leafing.Web;

namespace Blog.Biz.Helpers
{
    public class NoPermissionException : WebException
    {
		public NoPermissionException() : base("Setting Error.") { }
		public NoPermissionException(string errorMessage) : base(errorMessage) { }
        public NoPermissionException(string msgFormat, params object[] os) : base(String.Format(msgFormat, os)) { }
        protected NoPermissionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public NoPermissionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
