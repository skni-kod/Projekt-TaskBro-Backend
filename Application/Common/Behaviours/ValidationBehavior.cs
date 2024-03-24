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
    private readonly IValidator<TRequest> _validator;
    private List<ValidationFailure> _errors;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validation = await _validator.ValidateAsync(request, cancellationToken);

        if (validation.IsValid)
        {
            return await next();
        }
        
        var errorMessages = new List<string>();
        foreach (var error in validation.Errors)
        {
            errorMessages.Add($"{error.PropertyName}: {error.ErrorMessage}");
        }
        var errorMessage = string.Join(", ", errorMessages);
        throw new BadRequestException(errorMessage);
    }
    
    private List<ValidationFailure> Errors {
        get => _errors;
        set {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            // Ensure any nulls are removed and the list is copied
            // to be consistent with the constructor below.
            _errors = value.Where(failure => failure != null).ToList();;
        }
    }
}