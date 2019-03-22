using System;

namespace DesignPatternSamples.Exceptions
{
    /// <summary>
    /// 消费者异常类
    /// </summary>
    public class ConsumerException : Exception
    {
        public ConsumerException(string message) : base(message)
        {

        }

        public ConsumerException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public ConsumerException(string format, params object[] args) : base(string.Format(format, args))
        {

        }
    }
}