using Carter;
using ManagerAccount.Models;
using ManagerAccount.Models.Requests;
using ManagerAccount.Presenter.Models.Requests;
using ManagerAccount.Presenter.Models.Responses;
using ManagerAccount.Repositories.DataAccess;
using ManagerAccount.UseCases.Abstractions;
using ManagerAccount.UseCases.Abstractions.Entities;
using ManagerAccount.UseCases.Dtos;
using ManagerAccount.UseCases.Entities;
using ManagerAccount.UseCases.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAccount.Presenter;

public class ManagerEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
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

        app.MapPost("order/create",[Authorize] async (HttpContext context, OrderRequest request, [FromServices] IOrderService orderService) =>
        {
            var managerId = long.Parse(context.User.FindFirst("id")!.Value);
            var result = await orderService.CreateOrder(new OrderDto
            {
                ClientEmail = request.Email,
                OrderDetails = new OrderDetailsDto
                {
                    TypeOfProduct = (TypeOfProductDto)request.OrderDetails.TypeOfProduct,
                    Value = request.OrderDetails.Value
                },
                ManagerId = managerId
            });
            return Results.Ok(new ApiResponse<PrepareOrderResponse?>(
                result.IsSuccess,
                result.Error,
                result.ErrorCode,
                result.IsSuccess
                    ? new PrepareOrderResponse
                    {
                        Id = (long)result.Data.Id!,
                        ManagerId = result.Data.ManagerId,
                        Value = result.Data.OrderDetails.Value,
                        TypeOfProduct = (TypeOfProduct)result.Data.OrderDetails.TypeOfProduct,
                        Status = (OrderStatus)result.Data.OrderStatus
                    }
                    : null));
        });
    }
}