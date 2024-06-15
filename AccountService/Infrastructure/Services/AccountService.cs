using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Presentation.ViewModels;
using AutoMapper;

namespace AccountService.Infrastructure.Services
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;
        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;

        }

        public async Task CreateAccountAsync(AccountRequestViewModel accountDTO)
        {
            var account = _mapper.Map<Account>(accountDTO);

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

        public async Task<AccountResponseViewModel> GetAccountByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Account ID must be greater than zero.");
            }

            try
            {
                Account account = await _accountRepository.GetAccountByIDAsync(id);

                return _mapper.Map<AccountResponseViewModel>(account);
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

        public async Task UpdateAccountAsync(AccountRequestViewModel accountDTO)
        {
            var account = _mapper.Map<Account>(accountDTO);

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
