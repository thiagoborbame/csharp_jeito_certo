using GymErp.Domain.Subscriptions.Aggreates.Enrollments;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GymErp.Domain.Financial.Features.ProcessCharging.Consumers;

public class EnrollmentCreatedEventMassTransitConsumer(
    Handler processChargingHandler,
    ILogger<EnrollmentCreatedEventMassTransitConsumer> logger) : IConsumer<EnrollmentCreatedEvent>
{
    public async Task Consume(ConsumeContext<EnrollmentCreatedEvent> context)
    {
        var enrollmentId = context.Message.EnrollmentId;

        logger.LogDebug(
            "Consumer(MassTransit): EnrollmentCreatedEvent received for EnrollmentId {EnrollmentId}",
            enrollmentId);

        try
        {
            await processChargingHandler.HandleAsync(context.Message, context.CancellationToken);

            logger.LogInformation(
                "Consumer(MassTransit): ProcessCharging completed for EnrollmentId {EnrollmentId}",
                enrollmentId);
        }
        catch (Exception ex)
        {
            logger.LogWarning(
                ex,
                "Consumer(MassTransit): ProcessCharging failed for EnrollmentId {EnrollmentId}",
                enrollmentId);
            throw;
        }
    }
}
