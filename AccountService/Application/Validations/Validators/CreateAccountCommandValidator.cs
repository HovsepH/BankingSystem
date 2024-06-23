using AccountService.Application.Commands;
using FluentValidation;

namespace AccountService.Application.Validations.Validators
{
    public class CreateAccountCommandValidator : AbstractValidator<UpdateAccountQommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(qommand => qommand.account.Id).NotEmpty().WithMessage("Id is required.")
              .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(qommand => qommand.account.AccountNumber).NotEmpty().WithMessage("Account number is required.");

            RuleFor(qommand => qommand.account.Balance).GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative.");

            RuleFor(qommand => qommand.account.UserId).NotEmpty().WithMessage("User Id must be greater than zero.");
        }
    }
}