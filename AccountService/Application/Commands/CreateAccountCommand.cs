using AccountService.Domain.Entities;
using AccountService.Presentation.ViewModels;
using MediatR;

namespace AccountService.Application.Commands
{
    public record CreateAccountCommand(AccountRequestViewModel account) : IRequest;

}
