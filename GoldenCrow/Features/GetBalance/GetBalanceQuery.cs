using MediatR;

namespace Golden_Crow.Features.GetBalance
{
    public class GetBalanceQuery: IRequest<Result<decimal>>

    {
        public int UserId { get; set; }
        public string Currency { get; set; }

        public GetBalanceQuery(int userId, string currency)
        { 
            UserId = userId;
            Currency = currency;
        }

    }
}
