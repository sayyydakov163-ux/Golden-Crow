using Golden_Crow.Database;
using Golden_Crow.Models;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string login, string name, string password);
    }

    
}
