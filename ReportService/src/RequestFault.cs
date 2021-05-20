using System;
using System.Runtime.Serialization;

namespace com.bitscopic.src
{
    [Serializable]
    public class RequestFault
    {
        public RequestFault inner;
        public String message;
        public String errorCode;
        public object extraInfo;
        public String className = "RequestFault";

        public RequestFault() { }

        public RequestFault(String message)
        {
            this.message = message;
        }

        public RequestFault(String message, Exception innerExc)
        {
            this.message = message;
            this.inner = new RequestFault(innerExc);
        }

        public RequestFault(String message, String errorCode, Exception innerExc)
        {
            this.message = message;
            this.errorCode = errorCode;
            if (innerExc != null)
                this.inner = new RequestFault(innerExc);
        }

        public RequestFault(BitscopicBaseException hbe)
        {
            this.errorCode = hbe.errorCode;
            this.extraInfo = hbe.extraInfo;
            if (hbe.inner != null)
            {
                this.inner = new RequestFault(hbe.inner);
            }
            this.message = hbe.Message;
        }

        private RequestFault(Exception exc) // be sure to only use this privately so className is not overwritten on top level fault
        {
            if (exc.Message == null)
            {
                return;
            }
            this.message = exc.Message;
            this.className = exc.GetType().Name;
        }
    }
}