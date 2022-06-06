using Message_Service;
using Message_Service.MessageBroker;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddSingleton<IMessageHolder, MessageHolder>();
builder.Services.AddHostedService<Receiver>();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();

app.Run();