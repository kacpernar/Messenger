using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Messenger;

public class MessageProducer : IMessageProducer
{
    public Task SendMessage(Message message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var queueMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(queueMessage);
        channel.BasicPublish(exchange: "",
            routingKey: "queue",
            basicProperties: null,
            body: body);
        return Task.CompletedTask;
    }
}