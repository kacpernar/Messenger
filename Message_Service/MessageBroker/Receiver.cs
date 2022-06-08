using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Message_Service.MessageBroker;

public class Receiver : IHostedService
{
    private readonly IMessageHolder _messageHolder;
    private readonly IMessageProducer _messageProducer;
    private ConnectionFactory Factory { get; set; }
    private IConnection Connection { get; set; }
    private readonly IModel _channel;

    public Receiver(IMessageHolder messageHolder, IMessageProducer messageProducer)
    {
        _messageHolder = messageHolder;
        _messageProducer = messageProducer;
        Factory = new ConnectionFactory { HostName = "localhost" };
        Connection = Factory.CreateConnection();
        _channel = Connection.CreateModel();
        _channel.QueueDeclare(queue: "queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
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
            switch (message.MessageStatus)
            {
                case MessageStatus.None:
                    _messageHolder.MessageList.Add(message);
                    break;
                case MessageStatus.DeletedByUser:
                {
                    var index = _messageHolder.MessageList.FindIndex(m => m.Id == message.Id);
                    _messageHolder.MessageList[index].MessageStatus = MessageStatus.DeletedByUser;
                    break;
                }
                case MessageStatus.DeletedToEveryone:
                    var messageToChange = _messageHolder.MessageList.Find(m => m.Id == message.Id);
                    if (messageToChange != null)
                    {
                        _messageHolder.DeleteMessage(messageToChange);
                    }
                    break;
                case MessageStatus.Pending:
                    break;

                default:
                    _messageHolder.MessageList.Add(message);
                    break;
            }
            _messageProducer.SendMessage(message);
        };
        _channel.BasicConsume(queue: "queue",
            autoAck: true,
            consumer: consumer);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}