namespace Golden_Crow.DTOs
{
    public class LoginRequest
    {
        // Это запрос на вход DTO, который клиент отправляет на сервер, чтобы войти в систему.
        public string Login { get; set; }
        public string Password { get; set; }


    }
}
