using System.ComponentModel.DataAnnotations;

namespace Golden_Crow.DTOs.User
{
    public class LoginRequest
    {
        // Это запрос на вход DTO, который клиент отправляет на сервер, чтобы войти в систему.
        [Required(ErrorMessage = "Поле login обязательно")]
        [MinLength(3, ErrorMessage = "Минимальная длина логина от 3 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле Password обязательно")]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля 6 символов")]
        public string Password { get; set; }


    }
}
