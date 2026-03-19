using CSharpFunctionalExtensions;
using GymErp.Common;
using GymErp.Domain.Subscriptions.Aggreates.Enrollments;
using Microsoft.Extensions.Logging;

namespace GymErp.Domain.Subscriptions.Features.AddNewEnrollment;

public class Handler(
    EnrollmentRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<Handler> logger)
{
    public async Task<Result<Guid>> HandleAsync(Request request)
    {
        logger.LogDebug("AddNewEnrollment: creating Enrollment aggregate for Email {Email}", request.Email);

        // FastEndpoints fornece o CancellationToken no endpoint, mas aqui o handler é resolvido via DI.
        // Para evitar falhas de resolução no Autofac, usamos CancellationToken.None neste nível.
        var cancellationToken = CancellationToken.None;

        var enrollmentResult = Enrollment.Create(
            request.Name,
            request.Email,
            request.Phone,
            request.Document,
            request.BirthDate,
            request.Gender,
            request.Address
        );

        if (enrollmentResult.IsFailure)
        {
            logger.LogWarning("AddNewEnrollment: enrollment validation failed: {Error}", enrollmentResult.Error);
            return Result.Failure<Guid>(enrollmentResult.Error);
        }

        var enrollment = enrollmentResult.Value;
        logger.LogDebug("AddNewEnrollment: enrollment created with Id {Id}, persisting", enrollment.Id);

        await repository.AddAsync(enrollment, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        logger.LogInformation("AddNewEnrollment: enrollment persisted with Id {Id}", enrollment.Id);
        return Result.Success(enrollment.Id);
    }
} 