using System.Text;
using RabbitMQ.Client;

namespace Messenger;

public class MessageProducer : IMessageProducer
{
    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "logs",
            routingKey: "",
            basicProperties: null,
            body: body);
        Console.WriteLine(" [x] Sent {0}", message);
    }
}