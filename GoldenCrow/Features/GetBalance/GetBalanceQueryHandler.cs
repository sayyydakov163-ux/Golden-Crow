using Golden_Crow.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.Features.GetBalance
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, Result<decimal>>
    {
        private readonly ApplicationDbContext _context;

        public GetBalanceQueryHandler(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<Result<decimal>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == request.UserId && a.Currency == request.Currency, cancellationToken);
            return account == null ? Result<decimal>.Failure("Аккаунт не найден"):
            Result<decimal>.Success(account.Balance);
        }
    }
}
