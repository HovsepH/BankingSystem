using AccountService.Application.Commands;
using AccountService.Application.Interfaces;
using AccountService.Application.Queries;
using AccountService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> CreateAccountAsync([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _mediator.Send(new CreateAccountCommand(account));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create account.");
                return StatusCode(500, "Failed to create account. Please try again later.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetAccountByIdAsync(int id)
        {
            try
            {
                var account = await _mediator.Send(new GetAccountByIdQuery(id));
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get account with ID: {id}");
                return StatusCode(500, $"Failed to get account with ID: {id}. Please try again later.");
            }
        }

                                                                    
        [HttpPost("Transfer/{sourceAccountId}/{destinationAccountId}/{amount}")]
        public async Task<IActionResult> TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount)
        {
            try
            {
                await _mediator.Send(new TransferQommand(sourceAccountId, destinationAccountId, amount));
                return Ok("Transfer completed successfully.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to transfer amount.");
                return StatusCode(500, "Failed to transfer amount. Please try again later.");
            }
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAccountAsync([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _mediator.Send(new UpdateAccountQommand(account));
                return Ok("Account updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update account with ID: {account.Id}");
                return StatusCode(500, $"Failed to update account with ID: {account.Id}. Please try again later.");
            }
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAccountAsync(int id)
        {
            try
            {
                await _mediator.Send(new DeleteAccountQommand(id));
                return Ok("Account deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete account with ID: {id}");
                return StatusCode(500, $"Failed to delete account with ID: {id}. Please try again later.");
            }
        }
    }
}
