using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;


namespace AccountService.Infrastructure.Repositories
{

    public class AccountRepository : IAccountRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public AccountRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreateAccountAsync(Account entity)
        {
            string query = "INSERT INTO Accounts (AccountNumber, Balance, UserId) VALUES (@AccountNumber, @Balance, @UserId)";

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetMyDBConnection())
            {
                var cmd = connection.CreateCommand();

                cmd.CommandText = query;

                cmd.Parameters.Add(new SqlParameter("@AccountNumber", entity.AccountNumber));

                cmd.Parameters.Add(new SqlParameter("@Balance", entity.Balance));

                cmd.Parameters.Add(new SqlParameter("@UserId", entity.UserId));

                await connection.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

            }

        }

        public async Task TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount)
        {
            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetMyDBConnection())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await DebitAccountAsync(connection, transaction, sourceAccountId, amount);
                        await CreditAccountAsync(connection, transaction, destinationAccountId, amount);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Transfer operation failed.", ex);
                    }
                }
            }

        }

        public async Task DebitAccountAsync(SqlConnection conn, SqlTransaction transaction, int accountId, decimal amount)
        {
            string query = "UPDATE Accounts SET Balance = Balance - @Amount WHERE Id = @AccountId AND Balance >= @Amount";

            var cmd = conn.CreateCommand();

            cmd.Transaction = transaction;

            cmd.CommandText = query;

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("@Amount", amount));

            cmd.Parameters.Add(new SqlParameter("@AccountId", accountId));

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Insufficient balance for debit operation.");
            }

        }

        public async Task CreditAccountAsync(SqlConnection connection, SqlTransaction transaction, int accountId, decimal amount)
        {
            string query = "UPDATE Accounts SET Balance = Balance + @Amount WHERE Id = @AccountId";

            var cmd = connection.CreateCommand();

            cmd.Transaction = transaction;

            cmd.CommandText = query;

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("@Amount", amount));

            cmd.Parameters.Add(new SqlParameter("@AccountId", accountId));

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<Account> GetAccountByIDAsync(object id)
        {
            if (id is null || !int.TryParse(id.ToString(), out int Id))
            {
                throw new ArgumentException("Invalid account Id");
            }

            string query = "SELECT * FROM Accounts WHERE Id = @Id";

            using (var connection = (SqlConnection)_connectionFactory.GetMyDBConnection() )
            {
                await connection.OpenAsync();

                var cmd = connection.CreateCommand();

                cmd.CommandText = query;

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@Id", Id));

                Account account = null;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        account = new Account
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),

                            AccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber")),

                            Balance = reader.GetDecimal(reader.GetOrdinal("Balance")),

                            UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                        };
                    }
                }

                return account;
            }
        }

        public async Task DeleteAccountAsync(object id)
        {
            if (id is null || !int.TryParse(id.ToString(), out int Id))
            {
                throw new ArgumentException("Invalid account Id");
            }


            using (var connection = (SqlConnection)_connectionFactory.GetMyDBConnection())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteTransactionsQuery = "DELETE FROM Transactions WHERE SourceAccountId=@Id OR DestinationAccountId=@Id ";

                        var deleteTransactionsCmd = connection.CreateCommand();

                        deleteTransactionsCmd.Transaction = transaction;

                        deleteTransactionsCmd.CommandText = deleteTransactionsQuery;

                        deleteTransactionsCmd.CommandType = CommandType.Text;

                        deleteTransactionsCmd.Parameters.Add(new SqlParameter("@Id", Id));

                        await deleteTransactionsCmd.ExecuteNonQueryAsync();



                        string deleteAccountQuery = "DELETE FROM Accounts WHERE Id = @Id";

                        var deleteAccountCmd = connection.CreateCommand();

                        deleteAccountCmd.Transaction = transaction;

                        deleteAccountCmd.CommandText = deleteAccountQuery;

                        deleteAccountCmd.CommandType = CommandType.Text;

                        deleteAccountCmd.Parameters.Add(new SqlParameter("@Id", Id));

                        await deleteAccountCmd.ExecuteNonQueryAsync();

                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception("Error occurred while deleting the account", ex);
                    }
                }
            }

        }


        public async Task UpdateAccountAsync(Account entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException();
            }

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetMyDBConnection())
            {
                await connection.OpenAsync();

                string query = "UPDATE Accounts SET AccountNumber = @AccountNumber, Balance = @Balance, UserId = @UserId WHERE Id = @Id";

                var cmd = connection.CreateCommand();

                cmd.CommandText = query;

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@Id", entityToUpdate.Id));

                cmd.Parameters.Add(new SqlParameter("@AccountNumber", entityToUpdate.AccountNumber));

                cmd.Parameters.Add(new SqlParameter("@Balance", entityToUpdate.Balance));

                cmd.Parameters.Add(new SqlParameter("@UserId", entityToUpdate.UserId));

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while updating the account", ex);
                }
            }
        }
    }
}
