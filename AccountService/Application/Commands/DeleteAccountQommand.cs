using MediatR;

namespace AccountService.Application.Commands
{
    public record DeleteAccountQommand(int id):IRequest;
  
}
