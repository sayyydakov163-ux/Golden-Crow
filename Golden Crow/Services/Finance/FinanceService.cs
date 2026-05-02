using System.Threading.Tasks;
using Golden_Crow.Database;
using Golden_Crow.DTOs.Finance;
using Golden_Crow.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;



namespace Golden_Crow.Services.Finance
{
    public class FinanceService : IFinanceService
    {
        private readonly ApplicationDbContext _dbContext;

        public FinanceService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task <Result<decimal>> GetBalanceAsync(string token)
        { 
            var session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Token == token);
            if (session == null)
            {
                return Result<decimal>.Failure("Пользователь не авторизован");
            }

            var user = await _dbContext.Users.FirstAsync(u => u.Id == session.UserId);
            var account = await _dbContext.Accounts.FirstAsync(a => a.UserId == session.UserId);
            return Result<decimal>.Success(account.Balance);
        
        }

        public async Task<Result> DepositAsync(string token, decimal amount)
        {
            
            var session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Token == token);
            if (session == null)
            {
                return Result.Failure("Пользователь не авторизован");
            }

            
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == session.UserId);
            if (user == null)
            {
                return Result.Failure("Пользователь не найден");
            }

            
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (account == null)
            {
                return Result.Failure("Аккаунт не найден");
            }

            
            account.Balance += amount;
            await _dbContext.SaveChangesAsync();

            return Result.Success();
        }


        public async Task<Result> TransferAsync(string token, string receiverLogin, decimal amount)
        {
            var session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Token == token);
            if (session == null)
            {
                return Result.Failure("Пользователь не авторизован");
            }

            var fromUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == session.UserId);
            if (fromUser == null)
            {
                return Result.Failure("Пользователь не найден");
            }

            var fromAccount = await _dbContext.Accounts.FirstOrDefaultAsync(y => y.UserId == fromUser.Id);
            if (fromAccount == null)
            {
                return Result.Failure("Аккаунт не найден");

            }

            var toUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == receiverLogin);
            if (toUser == null)
            {
                return Result.Failure("Получатель не найден");
            }

            var toAccount = await _dbContext.Accounts.FirstOrDefaultAsync(x=> x.UserId == toUser.Id);

            if (fromAccount.Balance < amount)
            {
                return Result.Failure("Недостаточно средств");
            }

            fromAccount.Balance -= amount;
            toAccount!.Balance += amount;

            var transaction = new Transaction
            {
                ReceiverAccountId = toAccount.Id,
                SenderAccountId = fromAccount.Id,
                Amount = amount,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return Result.Success();

         
        }


        public async Task<Result<IEnumerable<TransactionHistoryResponse>>> GetTransactionHistoryAsync(string token, DateTime? dateFrom, DateTime? dateTo, int skip, int take)
        {
            if(dateFrom != null && dateTo != null && dateFrom> dateTo)
            {
                return Result<IEnumerable<TransactionHistoryResponse>>.Failure("Некорректный диапазон дат");
            }

            var session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Token == token);
            if (session == null)
            {
                return Result<IEnumerable<TransactionHistoryResponse>>.Failure("Пользователь не авторизован");
            }
            
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(y => y.UserId == session.UserId);

            var transactions = _dbContext.Transactions.Where(x => x.SenderAccountId == account!.Id || x.ReceiverAccountId == account.Id);

            if (dateFrom != null)
            {
                transactions = transactions.Where(x => x.CreatedAt >= dateFrom.Value);
            }
            if (dateTo != null)
            {
                transactions = transactions.Where(x => x.CreatedAt <= dateTo.Value);
            }

            transactions = transactions.Skip(skip).Take(take);

            var dbTransactions = await transactions.ToListAsync();

            var result = new List<TransactionHistoryResponse>();
            var allSenders = transactions.Select(x => x.SenderAccountId);
            var allReceivers = transactions.Select(x => x.ReceiverAccountId);
            var allAccounts = allSenders.ToHashSet();
            foreach (var receiver in allReceivers)
            { 
                allAccounts.Add(receiver);
            }

            var names = await _dbContext.Accounts.Where(x => allAccounts.Contains(x.Id))
                .Join(_dbContext.Users,
                acc => acc.UserId,
                u => u.Id,
                (acc, u) => new
                {
                    Name = u.Name,
                    AccId = acc.Id,
                }).ToDictionaryAsync(x=> x.AccId);


            foreach (var transaction in transactions)
            {

                var senderName = names[transaction.SenderAccountId].Name;
                var receiverName = names[transaction.ReceiverAccountId].Name;
                result.Add(new TransactionHistoryResponse
                {
                    SenderName = senderName,
                    ReceiverName = receiverName,
                    Amount = transaction.Amount,
                    Date = transaction.CreatedAt
                });
            }

            return result;

        }
    }
}
