using AccountService.Domain.Entities;
using AccountService.Presentation.ViewModels;
using MediatR;

namespace AccountService.Application.Commands
{
    public record UpdateAccountQommand(AccountRequestViewModel account):IRequest;
   
}
