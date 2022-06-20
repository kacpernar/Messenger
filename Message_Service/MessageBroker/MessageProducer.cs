using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Message_Service.MessageBroker;

public class MessageProducer : IMessageProducer
{
    private readonly ILogger<MessageProducer> _logger;

    public MessageProducer(ILogger<MessageProducer> logger)
    {
        _logger = logger;
    }
    public void SendMessage(Message message)
    {
        _logger.LogInformation(message: $"Sending message: {message.Text}");
        //var factory = new ConnectionFactory { HostName = "localhost" };
        var factory = new ConnectionFactory { HostName = "rabbitmq" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

        var xd = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(xd);
        channel.BasicPublish(exchange: "logs",
            routingKey: "",
            basicProperties: null,
            body: body);
    }
}