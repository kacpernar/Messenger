using Messenger;
using Messenger.Blazor.Services;
using Messenger.Blazor.Hubs;
using Microsoft.AspNetCore.ResponseCompression;


var builder = WebApplication.CreateBuilder(args);

#region ConfigureServices
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddTransient<IMessageService, MessageService>();

builder.Services.AddScoped<IMessageHolder, MessageHolder>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddHostedService<Receiver>();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();

//SignalR
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
#endregion

var app = builder.Build();

#region app_Configure
app.UseResponseCompression();

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
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToPage("/_Host");

app.Run();
#endregion