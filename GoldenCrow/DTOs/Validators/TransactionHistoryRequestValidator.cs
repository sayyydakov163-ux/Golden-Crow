using System.Transactions;
using FluentValidation;
using Golden_Crow.DTOs.Finance;
using Microsoft.AspNetCore.Rewrite;

namespace Golden_Crow.DTOs.Validators
{
    public class TransactionHistoryRequestValidator: AbstractValidator<TransactionHistoryRequest>
    {

        public TransactionHistoryRequestValidator() 
        {
            RuleFor(x => x.Limit)
                 .GreaterThan(0).WithMessage("Значение Limit должно быть не меньше 1");
            RuleFor(x => x.Offset)
                .GreaterThanOrEqualTo(0).WithMessage("Значение offset не может быть отрицательным");
        
        
        }

    }
}
