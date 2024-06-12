using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application.Queries
{
    public record GetAccountByIdQuery(int id) : IRequest<Account>;
}
