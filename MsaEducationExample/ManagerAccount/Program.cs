using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Carter;
using ManagerAccount.Models.Requests;
using ManagerAccount.Repositories.DataAccess;
using ManagerAccount.Repositories.Frameworks.HubIntegrations;
using ManagerAccount.UseCases.Abstractions;
using ManagerAccount.UseCases.Entities.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IHubService, HubService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Hub:BaseUrl"]!);
});

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddCarter();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();


var app = builder.Build();

app.MapCarter();



app.Run();