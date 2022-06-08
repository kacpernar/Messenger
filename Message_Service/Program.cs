using Message_Service;
using Message_Service.MessageBroker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMessageHolder, MessageHolder>();
builder.Services.AddHostedService<Receiver>();
builder.Services.AddTransient<IMessageProducer, MessageProducer>();

var app = builder.Build();



app.Run();