using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application.Commands
{
    public record CreateAccountCommand(Account account) : IRequest;

}
