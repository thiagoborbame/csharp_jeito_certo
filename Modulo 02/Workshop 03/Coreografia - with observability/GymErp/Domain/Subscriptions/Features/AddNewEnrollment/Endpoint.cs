using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace GymErp.Domain.Subscriptions.Features.AddNewEnrollment;

public class Endpoint(Handler handler, ILogger<Endpoint> logger) : Endpoint<Request, Guid>
{
    public override void Configure()
    {
        Post("/api/enrollments");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        logger.LogInformation("AddNewEnrollment request received");
        logger.LogDebug(
            "AddNewEnrollment request received with Name {Name} and Email {Email}",
            req.Name,
            req.Email);

        var result = await handler.HandleAsync(req);
        if (result.IsFailure)
        {
            logger.LogWarning("AddNewEnrollment failed: {Error}", result.Error);
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        logger.LogDebug("AddNewEnrollment succeeded for EnrollmentId {EnrollmentId}", result.Value);
        await SendOkAsync(result.Value, ct);
        logger.LogInformation("AddNewEnrollment completed for EnrollmentId {EnrollmentId}", result.Value);
    }
}