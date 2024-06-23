using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using TransactionService.Models;

namespace TransactionService
{
    public class TransactionRepository
    {
        public async Task<List<TransactionModel>> GetTransactionsByUserIdAsync(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentException("Id can't be null");
            }

            string connectionString = "Data Source=LAPTOP-ERU8URKE\\SQLEXPRESS01; Integrated Security = true; Initial Catalog = BankingSystem; Encrypt = False;";
            string userId = obj.ToString();

            List<TransactionModel> transactions = new List<TransactionModel>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Transactions WHERE (SourceAccountId IN (SELECT Id FROM Accounts WHERE UserId = @UserId) OR DestinationAccountId IN (SELECT Id FROM Accounts WHERE UserId =@UserId));";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@UserId", userId));

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                TransactionModel transaction = new TransactionModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                    SourceAccountId = reader.GetInt32(reader.GetOrdinal("SourceAccountId")),
                                    DestinationAccountId = reader.GetInt32(reader.GetOrdinal("DestinationAccountId")),
                                    TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate"))
                                };

                                transactions.Add(transaction);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exception (log it, rethrow it, etc.)
                throw new Exception("An error occurred while fetching transactions.", ex);
            }

            return transactions;
        }

    }
}

