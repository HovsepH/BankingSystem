using AccountService.Application.Interfaces;
using AccountService.Application.Queries;
using AccountService.Domain.Entities;
using AccountService.Presentation.ViewModels;
using MediatR;

namespace AccountService.Application.Handlers
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, AccountResponseViewModel>
    {
        private readonly IAccountService _accountService;
        public GetAccountByIdHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<AccountResponseViewModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            return await _accountService.GetAccountByIdAsync(request.id);
        }
    }
}
