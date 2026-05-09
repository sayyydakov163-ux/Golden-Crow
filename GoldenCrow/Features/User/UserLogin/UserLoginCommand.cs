using Golden_Crow.Database;
using Golden_Crow.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.Features.User.UserLogin
{
    public class UserLoginCommand:IRequest<Result<string>>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserLoginCommand(string login, string password)         
        {
            Login = login;
            Password = password;
        
        }


    }

    
}
