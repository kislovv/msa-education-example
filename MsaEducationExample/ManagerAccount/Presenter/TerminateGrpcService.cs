using Grpc.Core;
using Logistic.Status.Proto;
using ManagerAccount.Repositories.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ManagerAccount.Presenter;

public class TerminateGrpcService(AppDbContext appDbContext, ILogger<TerminateGrpcService> logger) : Logistic.Status.Proto.TerminateStatusService.TerminateStatusServiceBase
{
    public override async Task<SendStatusResponse> SendTerminateStatus(SendStatusRequest request, ServerCallContext context)
    {
        logger.LogInformation("{Message}. OrderId: {OrderId}. OrderStatus: {OrderStatus}: Time: {Time}",
            request.Message, request.OrderId, request.OrderStatus.ToString(), request.TimeToSend.ToString());

        var managerWithOrder =
            await appDbContext.Managers.FirstOrDefaultAsync(m => m.OrderIds.Any(om => om.Id == request.OrderId));

        if (managerWithOrder is not null)
        {
            managerWithOrder.OrderIds.RemoveAll(om => om.Id == request.OrderId);
            appDbContext.Update(managerWithOrder);
        }

        return new SendStatusResponse();
    }
}