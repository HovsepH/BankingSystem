using AccountService.Application.Commands;
using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application.Handlers
{
    public class TransferHandler : IRequestHandler<TransferQommand>
    {
        private readonly IAccountService _accountService;
        public TransferHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(TransferQommand request, CancellationToken cancellationToken)
        {
            await _accountService.TransferAsync(request.sourceAccountId, request.destinationAccountId, request.amount);
        }
    }
}
