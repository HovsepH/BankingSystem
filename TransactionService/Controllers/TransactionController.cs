using Microsoft.AspNetCore.Mvc;

namespace TransactionService.Controllers
{
    [ApiController]
    [Route("/api/transaction")]
    public class TransactionController : Controller
    {
        TransactionRepository _transactionRepository;

        public TransactionController(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }


        [HttpGet("get-by-user-id/{Id}")]
        public async Task<IActionResult> GetTransactionsByUserIdAsync(string Id)
        {
            var transctions = await _transactionRepository.GetTransactionsByUserIdAsync(Id);
            
            return Ok(transctions);
        }
    }
}
