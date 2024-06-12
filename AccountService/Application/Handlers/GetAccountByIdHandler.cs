using AccountService.Application.Interfaces;
using AccountService.Application.Queries;
using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application.Handlers
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, Account>
    {
        private readonly IAccountService _accountService;
        public GetAccountByIdHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            return await _accountService.GetAccountByIdAsync(request.id);
        }
    }
}
