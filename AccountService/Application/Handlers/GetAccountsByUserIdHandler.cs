using AccountService.Application.Interfaces;
using AccountService.Application.Queries;
using AccountService.Presentation.ViewModels;
using MediatR;

namespace AccountService.Application.Handlers
{
    public class GetAccountsByUserIdHandler : IRequestHandler<GetAccountsByUserIdQuery, List<AccountResponseViewModel>>
    {
        private readonly IAccountService _accountService;
        public GetAccountsByUserIdHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<List<AccountResponseViewModel>> Handle(GetAccountsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _accountService.GetAccountsByUserIdAsync(request.id);
        }
    }
}
