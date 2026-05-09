namespace Golden_Crow.Database.RabbitMQ
{
    public interface IMessageProducer
    {
        Task SendMesageAsync<T>(T message, CancellationToken token = default);  

    }
}
