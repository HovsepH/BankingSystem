using AccountService.Application.Commands;
using FluentValidation;

namespace AccountService.Application.Validations.Validators
{
    public class UpdateAccountQommandValidator : AbstractValidator<UpdateAccountQommand>
    {
        public UpdateAccountQommandValidator()
        {
            RuleFor(qommand => qommand.account.Id).NotEmpty().WithMessage("Id is required.")
              .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(qommand => qommand.account.AccountNumber).NotEmpty().WithMessage("Account number is required.");

            RuleFor(qommand => qommand.account.Balance).GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative.");

            RuleFor(qommand => qommand.account.UserId).GreaterThan(0).WithMessage("User Id must be greater than zero.");
        }
    }
}
