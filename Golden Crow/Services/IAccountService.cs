namespace Golden_Crow.Services;
using Golden_Crow.Database;
using Golden_Crow.Models;
using Microsoft.EntityFrameworkCore;


    public interface IAccountService
    {
        Task CreateAccountAsync(string login);
    }

    


    

