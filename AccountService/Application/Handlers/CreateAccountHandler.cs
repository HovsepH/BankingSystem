using AccountService.Application.Commands;
using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application.Handlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
    {
        private readonly IAccountService _accountService;

        public CreateAccountHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            await _accountService.CreateAccountAsync(request.account);
        }
    }
}