using AccountService.Application.Commands;
using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application.Handlers
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountQommand>
    {
        private readonly IAccountService _accountService;

        public DeleteAccountHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(DeleteAccountQommand request, CancellationToken cancellationToken)
        {
            await _accountService.DeleteAccountAsync(request.id);
        }
    }
}
