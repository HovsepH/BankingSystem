using MediatR;

namespace AccountService.Application.Commands
{
    public record TransferQommand(int sourceAccountId, int destinationAccountId, decimal amount) : IRequest;

}
