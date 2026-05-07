using Golden_Crow.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.Features.Deposit
{
    public class DepositCommandHandler : IRequestHandler<DepositCommand, Result>
    {
        private readonly ApplicationDbContext _dbContext;

        public DepositCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                return Result.Failure("Пользователь не найден");
            }

            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
            if (account == null)
            {
                return Result.Failure("Аккаунт не найден");
            }

            account.Balance += request.Amount;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
