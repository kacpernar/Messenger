using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messenger;

public class Receiver : IHostedService
{
    private readonly IMessage _message;
    private ConnectionFactory Factory { get; set; }
    private IConnection Connection { get; set; }
    private readonly IModel _channel;

    private string QueueName { get; set; }

    public Receiver(IMessage message)
    {
        _message = message;
        Factory = new ConnectionFactory { HostName = "localhost" };
        Connection = Factory.CreateConnection();
        _channel = Connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
        QueueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: QueueName,
            exchange: "logs",
            routingKey: "");
        
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            _message.Text = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] {0}", _message.Text);
        };
        _channel.BasicConsume(queue: QueueName,
            autoAck: true,
            consumer: consumer);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}