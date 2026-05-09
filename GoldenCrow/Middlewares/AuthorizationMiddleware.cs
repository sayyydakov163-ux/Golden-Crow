using System.Net;
using Golden_Crow.Attributes;
using Golden_Crow.Database;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public AuthorizationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        { 
            _next = next;
            _scopeFactory = scopeFactory;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            var attribute = context.GetEndpoint()?.Metadata.GetMetadata<MyAuthorizeAttribute>();

            // Если атрибута нет - эндпоинт публичный, пропускаем
            if (attribute == null)
            {
                await _next(context);
                return;
            }

            // Получаем токен из заголовка
            var authHeader = context.Request.Headers[Constants.Authorization].FirstOrDefault();
            var token = authHeader?.Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var session = await dbContext.Sessions.FirstOrDefaultAsync(x => x.Token == token);

                // Проверяем: сессия существует и не истекла
                if (session == null || session.ExpiresAt < DateTime.UtcNow)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                context.Items[Constants.UserIdContextParameter] = session.UserId;
            }

            await _next(context);
        }



    }
}
