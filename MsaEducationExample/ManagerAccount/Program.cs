using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManagerAccount.DataAccess;
using ManagerAccount.Entities;
using ManagerAccount.Models.Requests;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();


var app = builder.Build();

app.MapPost("/client/signup", async (ClientRequest request, AppDbContext dbContext) =>
{
    await dbContext.Clients.AddAsync(new Client
    {
        Login = request.Login,
        Password = request.Password,
        Email = request.Email,
        Role = Roles.Client
    });
    
    await dbContext.SaveChangesAsync();
    
    return Results.Ok();
});

app.MapPost("/manager/signup", async (ManagerRequest request, AppDbContext dbContext) =>
{
    await dbContext.Managers.AddAsync(new Manager
    {
        Login = request.Login,
        Password = request.Password,
        FullName = request.Fullname,
        Role = Roles.Client
    });
    
    await dbContext.SaveChangesAsync();
    
    return Results.Ok();
});

app.MapPost("/signin", async (LoginRequest request, AppDbContext dbContext) =>
{
    var user = dbContext.Users
        .SingleOrDefault(u => u.Login == request.Login && u.Password == request.Password);
    
    if (user is null)
    {
        return Results.Unauthorized();
    }
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(app.Configuration["JwtAuth:Key"]!);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        }),
        //TODO: вынести время жизни в конфиг и добавить вариант с refresh token
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    
    return Results.Ok(tokenHandler.WriteToken(token));
});

app.Run();