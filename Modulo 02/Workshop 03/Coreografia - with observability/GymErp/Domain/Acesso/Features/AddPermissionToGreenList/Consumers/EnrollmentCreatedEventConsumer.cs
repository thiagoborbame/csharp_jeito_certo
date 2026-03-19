using GymErp.Domain.Acesso.Features.AddPermissionToGreenList;
using GymErp.Domain.Subscriptions.Aggreates.Enrollments;
using Microsoft.Extensions.Logging;

namespace GymErp.Domain.Acesso.Features.AddPermissionToGreenList.Consumers;

public class EnrollmentCreatedEventConsumer(
    AddPermissionToGreenListHandler handler,
    ILogger<EnrollmentCreatedEventConsumer> logger)
{
    public async Task HandleAsync(EnrollmentCreatedEvent message, CancellationToken cancellationToken)
    {
        // #region agent log
        GymErp.Common.DebugSessionLog.Write("D", "EnrollmentCreatedEventConsumer(Acesso).HandleAsync", "Consumer entered", new Dictionary<string, object?> { ["enrollmentId"] = message.EnrollmentId });
        // #endregion

        logger.LogDebug(
            "Consumer(Acesso): EnrollmentCreatedEvent received for EnrollmentId {EnrollmentId}",
            message.EnrollmentId);

        try
        {
            await handler.HandleAsync(message, cancellationToken);
            logger.LogInformation(
                "Consumer(Acesso): AddPermissionToGreenList completed for EnrollmentId {EnrollmentId}",
                message.EnrollmentId);
        }
        catch (Exception ex)
        {
            logger.LogWarning(
                ex,
                "Consumer(Acesso): AddPermissionToGreenList failed for EnrollmentId {EnrollmentId}",
                message.EnrollmentId);
            throw;
        }
    }
}

