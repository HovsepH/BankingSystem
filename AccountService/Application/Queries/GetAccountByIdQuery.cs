using AccountService.Domain.Entities;
using AccountService.Presentation.ViewModels;
using MediatR;

namespace AccountService.Application.Queries
{
    public record GetAccountByIdQuery(int id) : IRequest<AccountResponseViewModel>;
}
