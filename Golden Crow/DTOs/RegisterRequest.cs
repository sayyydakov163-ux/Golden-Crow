namespace Golden_Crow.DTOs
{
    public class RegisterRequest
    {
        // Это запрос на регистрацию DTO, который клиент отправляет на сервер, чтобы создать новый аккаунт.
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
