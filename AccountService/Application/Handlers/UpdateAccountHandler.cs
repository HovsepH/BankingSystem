using AccountService.Application.Commands;
using AccountService.Application.Interfaces;
using MediatR;
using Microsoft.Identity.Client;

namespace AccountService.Application.Handlers
{
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountQommand>
    {
        IAccountService _accountService;
        public UpdateAccountHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(UpdateAccountQommand request, CancellationToken cancellationToken)
        {
            await _accountService.UpdateAccountAsync(request.account);
        }
    }
}
