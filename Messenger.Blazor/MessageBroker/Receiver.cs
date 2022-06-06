﻿using System.Text;
using System.Text.Json;
using Messenger.Blazor.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messenger;

public class Receiver : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly EventService _eventService;
    private readonly IMessageService _messageService;
    private ConnectionFactory Factory { get; set; }
    private IConnection Connection { get; set; }
    private readonly IModel _channel;

    private string QueueName { get; set; }

    public Receiver(IServiceProvider serviceProvider, EventService eventService, IMessageService messageService)
    {
        _serviceProvider = serviceProvider;
        _eventService = eventService;
        _messageService = messageService;
        Factory = new ConnectionFactory { HostName = "localhost" };
        Connection = Factory.CreateConnection();
        _channel = Connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
        QueueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: QueueName,
            exchange: "logs",
            routingKey: "");
        
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var jsonString = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<Message>(jsonString);
            if (message == null) return;
            HandleMessage(message);

        };
        _channel.BasicConsume(queue: QueueName,
            autoAck: true,
            consumer: consumer);
        
        return Task.CompletedTask;
    }

    private Task HandleMessage(Message message)
    {
        return _messageService.UpdateMessagesList(message);
    }
}