using System.Threading.Tasks;
using Golden_Crow.DTOs.Finance;

namespace Golden_Crow.Services.Finance
{
    public interface IFinanceService
    {
        public Task<Result<decimal>> GetBalanceAsync(string token);
        public Task<Result> DepositAsync(string token, decimal amount);

        public Task<Result> TransferAsync(string token, string receiverLogin, decimal amount);
        public Task<Result<IEnumerable<TransactionHistoryResponse>>> GetTransactionHistoryAsync(string token, DateTime? dateFrom, DateTime? dateTo, int skip, int take);
    }
}
