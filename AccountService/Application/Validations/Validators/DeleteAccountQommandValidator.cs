using AccountService.Application.Commands;
using FluentValidation;

namespace AccountService.Application.Validations.Validators
{
    public class DeleteAccountQommandValidator:AbstractValidator<DeleteAccountQommand>
    {
        public DeleteAccountQommandValidator()
        {
            RuleFor(qommand => qommand.id).NotEmpty().WithMessage("Id is required.")
              .GreaterThan(0).WithMessage("Id must be greater than zero.");
        }
    }
}
