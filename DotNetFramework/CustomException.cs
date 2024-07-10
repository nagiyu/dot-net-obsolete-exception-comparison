using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework
{
    [Serializable]
    public class CustomException : Exception
    {
        public int ErrorCode { get; private set; }

        public CustomException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = info.GetInt32("ErrorCode");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ErrorCode", ErrorCode);
        }
    }
}
