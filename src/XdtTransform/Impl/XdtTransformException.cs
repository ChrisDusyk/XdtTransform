using System;
using System.Runtime.Serialization;

namespace XdtTransform.Impl
{
    [Serializable]
    public class XdtTransformException : Exception
    {
        public XdtTransformException()
        {
        }
        public XdtTransformException(String message)
            : base(message)
        {
        }
        public XdtTransformException(String message, Exception inner)
            : base(message, inner)
        {
        }
        protected XdtTransformException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
