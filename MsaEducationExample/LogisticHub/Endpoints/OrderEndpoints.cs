using Carter;
using LogisticHub.Models;
using MediatR;

namespace LogisticHub.Endpoints;

public class OrderEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/add", async (CreateOrderCommand createOrderCommand, IMediator mediator) =>
        {
            var result = await mediator.Send(createOrderCommand);
            
            return Results.Ok(result);
        });
    }
}