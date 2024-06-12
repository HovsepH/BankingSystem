using AccountService.Domain.Entities;

namespace AccountService.Application.Interfaces
{
    public interface IAccountService
    {
        public Task CreateAccountAsync(Account account);

        public Task<Account> GetAccountByIdAsync(int id);

        public Task TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount);

        public Task UpdateAccountAsync(Account account);

        public Task DeleteAccountAsync(int id);
    }
}
