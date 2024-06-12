using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Infrastructure.Services
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task CreateAccountAsync(Account account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            try
            {
                await _accountRepository.CreateAccountAsync(account);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create account. Please try again later.", ex);
            }
        }

        public async Task DeleteAccountAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Account ID must be greater than zero.");
            }

            try
            {
                await _accountRepository.DeleteAccountAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete account: {id}", ex);
            }
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Account ID must be greater than zero.");
            }

            try
            {
                return await _accountRepository.GetAccountByIDAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get account: {id}", ex);
            }
        }

        public async Task TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount)
        {
            if (sourceAccountId <= 0 || destinationAccountId <= 0 || amount <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            try
            {
                await _accountRepository.TransferAsync(sourceAccountId, destinationAccountId, amount);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during the transfer", ex);
            }
        }

        public async Task UpdateAccountAsync(Account account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            try
            {
                await _accountRepository.UpdateAccountAsync(account);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during the update", ex);
            }
        }
    }
}
