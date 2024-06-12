using AccountService.Domain.Entities;
using AccountService.Presentation.ViewModels;

namespace AccountService.Application.Interfaces
{
    public interface IAccountService
    {
        public Task CreateAccountAsync(AccountRequestViewModel account);

        public Task<AccountResponseViewModel> GetAccountByIdAsync(int id);

        public Task TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount);

        public Task UpdateAccountAsync(AccountRequestViewModel account);

        public Task DeleteAccountAsync(int id);
    }
}
