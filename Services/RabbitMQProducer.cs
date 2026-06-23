using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace TraineeManagement.api.Services;

public class RabbitMQProducer : IMessageProducer, IDisposable
{
    private readonly IConnection _connection;

    private readonly IChannel _channel;

    private const string QueueName = "file_processing_queue";

    private readonly IConfiguration _config;

    public RabbitMQProducer(IConfiguration config)
    {
        _config = config;
        var rabbitmq = _config.GetSection("RabbitMQ");
        var factory = new ConnectionFactory
        {
            HostName = rabbitmq["HostName"]!.ToString(),
            Port = 5672,
            UserName = rabbitmq["Username"]!.ToString(),
            Password = rabbitmq["Password"]!.ToString()
        };

        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

        _channel.QueueDeclareAsync(queue: QueueName, durable: true, exclusive: false, autoDelete: false).GetAwaiter().GetResult();
    }

    public void SendMessage<T>(T message)
    {
        string jsonString = JsonSerializer.Serialize(message);
        byte[] body = Encoding.UTF8.GetBytes(jsonString);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        _channel.BasicPublishAsync(
            exchange: "",
            routingKey: QueueName,
            mandatory: false,
            basicProperties: properties,
            body: body).GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}