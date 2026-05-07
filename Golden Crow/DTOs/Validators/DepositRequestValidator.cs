using FluentValidation;
using Golden_Crow.DTOs.Finance;

namespace Golden_Crow.DTOs.Validators
{
    public class DepositRequestValidator: AbstractValidator<DepositRequest>
    {
        public DepositRequestValidator() 
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма должна быть больше 0");


        }


    }
}
