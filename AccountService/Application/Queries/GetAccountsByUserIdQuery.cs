using AccountService.Presentation.ViewModels;
using MediatR;

namespace AccountService.Application.Queries
{
    public record GetAccountsByUserIdQuery(string id) : IRequest<List<AccountResponseViewModel>>;
}
