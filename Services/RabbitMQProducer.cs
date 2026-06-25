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

    private const string DlxExchange = "submissions.dlx";

    private const string DlqQueue = "submissions.dlq";

    private const string DlxRoutingKey = "submission.failed";

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
    }

    public async void SendMessage<T>(T message)
    {
        await _channel.ExchangeDeclareAsync(QueueName, ExchangeType.Topic, durable: true);
        await _channel.ExchangeDeclareAsync(DlxExchange, ExchangeType.Headers, durable: true);

        IDictionary<string, object?> queueArguments = new Dictionary<string, object?>()
        {
            {"x-dead-letter-exchange", DlxExchange},
            {"x-dead-letter-routing-key", DlxRoutingKey}
        };

        await _channel.QueueDeclareAsync(QueueName, durable: true, autoDelete: false, exclusive: false, arguments: queueArguments);
        await _channel.QueueDeclareAsync(DlqQueue, durable: true, exclusive: false, autoDelete: false);
        await _channel.QueueBindAsync(DlqQueue, DlxExchange, routingKey: DlxRoutingKey);

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