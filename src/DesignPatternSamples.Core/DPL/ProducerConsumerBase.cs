using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using DesignPatternSamples.DPL.Models;
using DesignPatternSamples.Exceptions;

namespace DesignPatternSamples.DPL
{
    /// <summary>
    /// 生产者/消费者抽象类
    /// </summary>
    public abstract class ProducerConsumerBase<TModel> where TModel : IProducerModel, new()
    {
        /// <summary>
        /// 线程安全的FIFO队列
        /// </summary>
        private readonly ConcurrentQueue<TModel> _queues = new ConcurrentQueue<TModel>();

        /// <summary>
        /// 压入<see cref="TModel"/>对象到队列结尾
        /// </summary>
        /// <param name="item"><see cref="TModel"/>对象</param>
        protected virtual void AddItemToQueues(TModel item)
        {
            _queues.Enqueue(item);
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        protected virtual void ClearQueues()
        {
            TModel item;
            while (_queues.Count > 0)
            {
                _queues.TryDequeue(out item);
            }
        }

        /// <summary>
        /// 生产者 业务逻辑
        /// </summary>
        protected abstract void Producer();

        /// <summary>
        /// 消费者 业务逻辑
        /// </summary>
        protected abstract void Consumer(TModel item);

        /// <summary>
        /// 双Task运行 生产者/消费者
        /// </summary>
        public virtual void Run()
        {
            Task producerTask = new Task(ProducerInternal);
            Task consumerTask = new Task(ConsumerInternal);

            producerTask.Start();
            consumerTask.Start();

            //等待消费者处理结束
            consumerTask.Wait();

            try
            {
                //如果内部有异常，主动引发异常，避免异常被吞
                producerTask.GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                //转换为生产者异常，以便外部使用者区分异常来源
                throw new ProducerException("生产者发生异常：" + ex.ToString(), ex);
            }

            try
            {
                //如果内部有异常，主动引发异常，避免异常被吞
                consumerTask.GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                //转换为消费者异常，以便外部使用者区分异常来源
                throw new ConsumerException("消费者发生异常：" + ex.ToString(), ex);
            }
        }

        /// <summary>
        /// 生产者 内部方法
        /// </summary>
        private void ProducerInternal()
        {
            try
            {
                //生产代码实现逻辑 do something...
                Producer();
            }
            finally
            {
                //无论如何都要压入 结束消费者的信号
                _queues.Enqueue(new TModel
                {
                    EndConsumer = true
                });
            }
        }

        /// <summary>
        /// 消费者 内部方法
        /// </summary>
        private void ConsumerInternal()
        {
            while (true)
            {
                //尝试获取新的消费
                TModel result;
                if (_queues.TryDequeue(out result) == false)
                {
                    Thread.Sleep(0);
                    continue;
                }

                if (result == null)
                {
                    Thread.Sleep(0);
                    continue;
                }

                //判断是否停止消费
                if (result.EndConsumer)
                {
                    //退出消费者
                    break;
                }

                //消费代码实现逻辑 do something...
                Consumer(result);
            }
        }
    }
}