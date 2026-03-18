using GymErp.Domain.Acesso.Features.AddPermissionToGreenList;
using GymErp.Domain.Subscriptions.Aggreates.Enrollments;

namespace GymErp.Domain.Acesso.Features.AddPermissionToGreenList.Consumers;

public class EnrollmentCreatedEventConsumer(AddPermissionToGreenListHandler handler)
{
    public Task HandleAsync(EnrollmentCreatedEvent message, CancellationToken cancellationToken)
    {
        return handler.HandleAsync(message, cancellationToken);
    }
}

