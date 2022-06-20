using System.IdentityModel.Tokens.Jwt;
using Messenger;
using Messenger.Blazor.Services;
using Messenger.Blazor.Hubs;
using Microsoft.AspNetCore.ResponseCompression;


var builder = WebApplication.CreateBuilder(args);

#region ConfigureServices
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Auth
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        // It is important this matches the actual URL of your identity server, not the Docker internal URL
        options.Authority = "http://localhost:5001";

        
        // This will allow the container to reach the discovery endpoint
        options.MetadataAddress = "http://identityserver/.well-known/openid-configuration";
        options.RequireHttpsMetadata = false;

        options.Events.OnRedirectToIdentityProvider = context =>
        {
            // Intercept the redirection so the browser navigates to the right URL in your host
            context.ProtocolMessage.IssuerAddress = "http://localhost:5001/connect/authorize";
            return Task.CompletedTask;
        };
        


        options.ClientId = "web";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.GetClaimsFromUserInfoEndpoint = true;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        

        options.SaveTokens = true;
    });

builder.Services.AddScoped<IMessageService, MessageService>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToPage("/_Host");

app.Run();
#endregion