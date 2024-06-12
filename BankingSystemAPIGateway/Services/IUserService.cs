using BankingSystemAPIGateway.Models;

namespace BankingSystemAPIGateway.Services
{
    public interface IUserService
    {
        Task<ResponseModel<List<UserModel>>> GetUsers();
    }
}
