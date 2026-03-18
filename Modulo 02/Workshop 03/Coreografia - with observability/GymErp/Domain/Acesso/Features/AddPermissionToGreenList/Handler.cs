using GymErp.Domain.Subscriptions.Aggreates.Enrollments;
using Microsoft.Extensions.Logging;

namespace GymErp.Domain.Acesso.Features.AddPermissionToGreenList;

public class AddPermissionToGreenListHandler(ILogger<AddPermissionToGreenListHandler> logger)
{
    public Task HandleAsync(EnrollmentCreatedEvent message, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "AddPermissionToGreenList operation executed successfully for EnrollmentId {EnrollmentId}",
            message.EnrollmentId);

        return Task.CompletedTask;
    }
}

