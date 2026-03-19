using GymErp.Domain.Subscriptions.Aggreates.Enrollments;
using Microsoft.Extensions.Logging;

namespace GymErp.Domain.Financial.Features.ProcessCharging.Consumers;

public class EnrollmentCreatedEventConsumer(
    Handler processChargingHandler,
    ILogger<EnrollmentCreatedEventConsumer> logger)
{
    public async Task HandleAsync(EnrollmentCreatedEvent message, CancellationToken cancellationToken)
    {
        // #region agent log
        GymErp.Common.DebugSessionLog.Write("D", "EnrollmentCreatedEventConsumer(Financial).HandleAsync", "Consumer entered", new Dictionary<string, object?> { ["enrollmentId"] = message.EnrollmentId });
        // #endregion
        logger.LogDebug(
            "Consumer(Financial): EnrollmentCreatedEvent received for EnrollmentId {EnrollmentId}",
            message.EnrollmentId);

        try
        {
            await processChargingHandler.HandleAsync(message, cancellationToken);

            logger.LogInformation(
                "Consumer(Financial): ProcessCharging completed for EnrollmentId {EnrollmentId}",
                message.EnrollmentId);
        }
        catch (Exception ex)
        {
            logger.LogWarning(
                ex,
                "Consumer(Financial): ProcessCharging failed for EnrollmentId {EnrollmentId}",
                message.EnrollmentId);
            throw;
        }
    }
}
