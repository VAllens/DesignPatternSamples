using System;

namespace DesignPatternSamples.Exceptions
{
    /// <summary>
    /// 生产者异常类
    /// </summary>
    public class ProducerException : Exception
    {
        public ProducerException(string message) : base(message)
        {

        }

        public ProducerException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public ProducerException(string format, params object[] args) : base(string.Format(format, args))
        {

        }
    }
}