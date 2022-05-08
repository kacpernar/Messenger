using Messenger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IMessage, Message>();
builder.Services.AddHostedService<Receiver>();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();

var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();