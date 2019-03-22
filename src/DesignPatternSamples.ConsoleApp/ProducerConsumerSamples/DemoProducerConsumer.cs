using System;
using System.Threading;
using DesignPatternSamples.DPL;

namespace DesignPatternSamples.ProducerConsumerSamples
{
    /// <summary>
    /// 生产者/消费者模板样例
    /// </summary>
    public class DemoProducerConsumer : ProducerConsumerBase<DemoModel>
    {
        /// <summary>
        /// 生产者业务逻辑
        /// </summary>
        protected override void Producer()
        {
            for (int index = 1; index <= 3; index++)
            {
                //业务代码...
                DemoModel model = new DemoModel
                {
                    Index = index
                };

                //假装业务逻辑处理了10秒钟
                Thread.Sleep(1000 * 10);

                //生产到第三个时挂了
                if (model.Index > 2)
                {
                    throw new Exception("生产死了！");
                }

                //这句代码是必须写的
                AddItemToQueues(model);

                Console.WriteLine("生产了：" + model.Index);
            }
        }

        protected override void Consumer(DemoModel model)
        {
            Console.WriteLine("消费了：" + model.Index);

            /* if (model.Index > 2)
            {
                throw new Exception("消费死了！");
            } */

            //下面的Sleep只是用来测试，当整个生产完全结束后，消费者是否还会继续走，并且等待消费者走完
            Thread.Sleep(1000 * 20);
        }
    }
}