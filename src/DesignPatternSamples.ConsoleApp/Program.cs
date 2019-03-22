using System;
using DesignPatternSamples.Exceptions;
using DesignPatternSamples.ProducerConsumerSamples;

namespace DesignPatternSamples
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //运行生产者/消费者
            RunProducerConsumer();

            Console.WriteLine("All DPL samples is done.");
            Console.ReadKey();
        }

        /// <summary>
        /// 运行生产者/消费者
        /// </summary>
        private static void RunProducerConsumer()
        {
            try
            {
                DemoProducerConsumer pc = new DemoProducerConsumer();
                pc.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("RunProducerConsumer方法, ");
                if (ex is ProducerException)
                {
                    Console.WriteLine("生产者发生了一些异常：" + ex.ToString());
                }
                else if (ex is ConsumerException)
                {
                    Console.WriteLine("消费者发生了一些异常：" + ex.ToString());
                }
                else
                {
                    Console.WriteLine("发生了未知异常：" + ex.ToString());
                }
            }
            finally
            {
                Console.WriteLine("RunProducerConsumer End!");
            }
        }
    }
}
