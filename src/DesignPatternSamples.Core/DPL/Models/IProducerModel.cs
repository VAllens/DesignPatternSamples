namespace DesignPatternSamples.DPL.Models
{
    /// <summary>
    /// 生产者模型基类
    /// </summary>
    public interface IProducerModel
    {
        /// <summary>
        /// 是否结束消费者的信号
        /// </summary>
        bool EndConsumer { get; set; }
    }
}