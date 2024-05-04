using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using FluentValidation;
using MediatR;
using Domain.Exceptions;
using FluentValidation.Results;

namespace Application.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validator.Any())
        {
            return await next();
        }

        var validationFailures = new List<ValidationFailure>();

        foreach (var validator in _validator)
        {
            var validationResul = await validator.ValidateAsync(request, cancellationToken);
            validationFailures.AddRange(validationResul.Errors);
        }

        if (validationFailures.Count == 0)
        {
            return await next();
        }

        var errorMessages = validationFailures.Select(e => e.ErrorMessage).ToList();
        var errorMessage = string.Join(", ", errorMessages);
        throw new BadRequestException(errorMessage);
    }
}