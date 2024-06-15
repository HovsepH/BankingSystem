using AccountService.Application.Commands;
using FluentValidation;

namespace AccountService.Application.Validations.Validators
{
    public class TransferQommandValidator : AbstractValidator<TransferQommand>
    {
        public TransferQommandValidator()
        {
            RuleFor(qommand => qommand.sourceAccountId).NotEmpty().WithMessage("Id is required.")
              .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(qommand => qommand.destinationAccountId).NotEmpty().WithMessage("Id is required.")
              .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(qommand => qommand.amount).GreaterThanOrEqualTo(0)
                .WithMessage("Amount cannot be negative.");

        }
    }
}
