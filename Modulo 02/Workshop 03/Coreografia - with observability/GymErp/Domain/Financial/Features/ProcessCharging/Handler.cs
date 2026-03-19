using GymErp.Common;
using GymErp.Domain.Subscriptions.Aggreates.Enrollments;
using Microsoft.Extensions.Logging;

namespace GymErp.Domain.Financial.Features.ProcessCharging;

public class Handler(ILogger<Handler> logger, IServiceBus serviceBus)
{
    public async Task HandleAsync(EnrollmentCreatedEvent message, CancellationToken cancellationToken)
    {
        logger.LogDebug("ProcessCharging: starting for EnrollmentId {EnrollmentId}", message.EnrollmentId);

        try
        {
            logger.LogInformation(
                "ProcessCharging: processing charging for enrollment {EnrollmentId}",
                message.EnrollmentId);

            // Simular processamento assíncrono
            await Task.Delay(100, cancellationToken);

            logger.LogInformation(
                "ProcessCharging: charging processed successfully for enrollment {EnrollmentId}",
                message.EnrollmentId);

            await serviceBus.PublishAsync(new ChargingProcessedEvent(message.EnrollmentId));
        }
        catch (Exception ex)
        {
            logger.LogWarning(
                ex,
                "ProcessCharging: failed for enrollment {EnrollmentId}",
                message.EnrollmentId);
            throw;
        }
    }
}

