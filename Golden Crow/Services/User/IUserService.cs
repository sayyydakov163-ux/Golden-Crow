using System.Threading.Tasks;
using Golden_Crow.Database;
using Golden_Crow.Models;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.Services.User
{
    public interface IUserService
    {
        Task<Result<string>> LoginAsync(string login, string password);

        Task<Result> RegisterAsync(string login, string name, string password);

    }


}
