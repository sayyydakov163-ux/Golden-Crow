using System.Threading.Tasks;
using Golden_Crow.DTOs.Finance;

namespace Golden_Crow.Services.Finance
{
    public interface IFinanceService
    {
        public Task<Result<decimal>> GetBalanceAsync(int userId);
        public Task<Result> DepositAsync(int userId, decimal amount);

        public Task<Result> TransferAsync(int FromUserId, string receiverLogin, decimal amount);
        public Task<Result<IEnumerable<TransactionHistoryResponse>>> GetTransactionHistoryAsync(int userId, DateTime? dateFrom, DateTime? dateTo, int skip, int take);
    }
}
