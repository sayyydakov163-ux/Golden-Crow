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
           var atribute = context.GetEndpoint()?.Metadata.GetMetadata<MyAuthorizeAttribute>();
            if (atribute != null)
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers[Constants.Autorization].FirstOrDefault()?.Split("").Last();
            if (string.IsNullOrEmpty(token))
            { 
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            }

            var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var session = await dbContext.Sessions.FirstOrDefaultAsync(x => x.Token == token);
            if (session != null || session.ExpiresAt < DateTime.UtcNow)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            context.Items[Constants.UserIdContextParameter] = session.UserId;

            await _next(context);
        }



    }
}
