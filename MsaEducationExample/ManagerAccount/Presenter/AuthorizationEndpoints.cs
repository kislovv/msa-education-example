using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Carter;
using ManagerAccount.Models.Requests;
using ManagerAccount.Presenter.Models.Requests;
using ManagerAccount.Repositories.DataAccess;
using ManagerAccount.UseCases.Entities;
using ManagerAccount.UseCases.Entities.Models;
using Microsoft.IdentityModel.Tokens;

namespace ManagerAccount.Presenter;

public class AuthorizationEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
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



        app.MapPost("/signin", async (
            LoginRequest request, AppDbContext dbContext, 
            IConfiguration configuration) =>
        {
            var user = dbContext.Users
                .SingleOrDefault(u => u.Login == request.Login && u.Password == request.Password);
    
            if (user is null)
            {
                return Results.Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JwtAuth:Key"]!);
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
    }
}