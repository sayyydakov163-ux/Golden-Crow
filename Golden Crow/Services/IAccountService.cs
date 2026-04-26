using System.Threading.Tasks;
using Golden_Crow.Database;
using Golden_Crow.Models;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.Services
{
    public interface IAccountService
    {
        Task CreateAccountAsync(string login);
    }
}