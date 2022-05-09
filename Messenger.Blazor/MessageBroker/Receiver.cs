using System.Text;
using System.Text.Json;
using Messenger.Blazor.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messenger;

public class Receiver : IHostedService
{
    private readonly IMessageHolder _messageHolder;
    private ConnectionFactory Factory { get; set; }
    private IConnection Connection { get; set; }
    private readonly IModel _channel;

    private string QueueName { get; set; }

    public Receiver(IMessageHolder messageHolder)
    {
        _messageHolder = messageHolder;
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
            var jsonString = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<Message>(jsonString);
            if (message == null) return;
            if (message.DeleteMessage)
            {
                var messageToDelete = _messageHolder.MessageList.Find(m => m.Id == message.Id);
                if (messageToDelete != null)
                {
                    _messageHolder.DeleteMessage(messageToDelete);
                }
            }
            else
            {
                _messageHolder.MessageList.Add(message);
            }
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