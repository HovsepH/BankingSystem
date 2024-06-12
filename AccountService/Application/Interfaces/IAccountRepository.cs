using AccountService.Domain.Entities;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AccountService.Application.Interfaces
{
    public interface IAccountRepository
    {
        public Task CreateAccountAsync(Account entity);

        public Task TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount);


        public Task DebitAccountAsync(SqlConnection conn, SqlTransaction transaction, int accountId, decimal amount);


        public Task CreditAccountAsync(SqlConnection connection, SqlTransaction transaction, int accountId, decimal amount);


        public Task<Account> GetAccountByIDAsync(object id);

        public Task DeleteAccountAsync(object id);

        public Task UpdateAccountAsync(Account entityToUpdate);

    }
}
