using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application.Commands
{
    public record UpdateAccountQommand(Account account):IRequest;
   
}
