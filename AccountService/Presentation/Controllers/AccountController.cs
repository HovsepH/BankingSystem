using AccountService.Application.Commands;
using AccountService.Application.Interfaces;
using AccountService.Application.Queries;
using AccountService.Domain.Entities;
using AccountService.Presentation.ViewModels;
using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Presentation.Controllers
{

    [ApiController]
    [Route("/api/account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IMediator _mediator;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IMediator mediator)
        {
            _logger = logger;

            _mediator = mediator;
        }

      
      
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountRequestViewModel accountDTO)
        {
            await _mediator.Send(new CreateAccountCommand(accountDTO));

            return Ok();
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetAccountByIdAsync(int id)
        {
            var accountDTO = await _mediator.Send(new GetAccountByIdQuery(id));

            if (accountDTO == null)
            {
                return NotFound();
            }

            return Ok(accountDTO);
        }


        [HttpGet("get-by-user-id/{id}")]
        public async Task<IActionResult> GetAccountsByUserIdAsync(string id)
        {
            List<AccountResponseViewModel> accountDTOs = await _mediator.Send(new GetAccountsByUserIdQuery(id));

            if (accountDTOs == null)
            {
                return NotFound();
            }

            return Ok(accountDTOs);
        }


        [HttpPost("Transfer/{sourceAccountId}/{destinationAccountId}/{amount}")]
        public async Task<IActionResult> TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount)
        {

            await _mediator.Send(new TransferQommand(sourceAccountId, destinationAccountId, amount));

            return Ok("Transfer completed successfully.");

        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAccountAsync([FromBody] AccountRequestViewModel accountDTO)
        {
            await _mediator.Send(new UpdateAccountQommand(accountDTO));

            return Ok("Account updated successfully.");
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAccountAsync(int id)
        {
            await _mediator.Send(new DeleteAccountQommand(id));

            return Ok("Account deleted successfully.");
        }
    }
}
