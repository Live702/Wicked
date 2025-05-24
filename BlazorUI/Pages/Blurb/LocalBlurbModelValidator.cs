using FluentValidation;
using WickedSchema;

namespace BlazorUI;

public class LocalBlurbModelValidator : AbstractValidator<BlurbModel>
{
    public LocalBlurbModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
} 