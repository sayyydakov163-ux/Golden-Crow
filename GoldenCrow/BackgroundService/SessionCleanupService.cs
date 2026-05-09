using Golden_Crow.Database;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.BackgroundService
{
    public class SessionCleanupService: Microsoft.Extensions.Hosting.BackgroundService
    {

        private static readonly TimeSpan Delay = TimeSpan.FromMinutes(10);

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SessionCleanupService> _logger;

        public SessionCleanupService(IServiceScopeFactory scopeFactory, ILogger<SessionCleanupService> logger)
        { 
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var now = DateTime.UtcNow;

                    var deletedCount = await db.Sessions
                        .Where(x => x.ExpiresAt <= now)
                        .ExecuteDeleteAsync(stoppingToken);

                    if (deletedCount > 0)
                    {
                        _logger.LogInformation("Удалено истекших сессий: {DeletedCount}", deletedCount);
                    }
                }

                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при очистке истекших сессий");
                }

                await Task.Delay(Delay, stoppingToken);

            
            }
        }

    }
}
