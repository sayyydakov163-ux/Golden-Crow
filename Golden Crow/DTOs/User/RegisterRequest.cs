using System.ComponentModel.DataAnnotations;

namespace Golden_Crow.DTOs.User
{
    public class RegisterRequest
    {
        // Это запрос на регистрацию DTO, который клиент отправляет на сервер, чтобы создать новый аккаунт.
        [Required(ErrorMessage = "Поле login обязательно")]
        [MinLength(3, ErrorMessage = "Минимальная длина логина от 3 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле Name обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Password обязательно")]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля 6 символов")]
        public string Password { get; set; }
    }
}
