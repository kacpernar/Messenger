
using System.Reflection;
using Messenger;
using Messenger.Blazor.Services;
using MediatR;
using Messenger.Blazor.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IMessageService).Assembly);
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddScoped<IRequestHandler<MessageRequestModel, MessageResponseModel>, MessageHandler>();

builder.Services.AddScoped<IMessageHolder, MessageHolder>();
builder.Services.AddScoped<IUser, User>();
builder.Services.AddSingleton<EventService>();
builder.Services.AddHostedService<Receiver>();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
