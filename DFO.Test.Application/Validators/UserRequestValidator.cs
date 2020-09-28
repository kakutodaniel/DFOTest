using FluentValidation;
using DFO.Test.Application.Contracts.User;

namespace DFO.Test.Application.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {


        public UserRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("name is required");

            RuleFor(x => x.Name)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.Name))
                .WithMessage("name must have up to 50 characters");

            RuleFor(x => x.Age)
                .GreaterThan(0)
                .WithMessage("age must be greater than 0");

            RuleFor(x => x.Address)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("address is required");

            RuleFor(x => x.Address)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithMessage("address must have up to 50 characters");


        }

    }
}
