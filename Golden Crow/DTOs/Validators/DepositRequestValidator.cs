using FluentValidation;
using Golden_Crow.DTOs.Finance;
using Golden_Crow.Models;

namespace Golden_Crow.DTOs.Validators
{
    public class DepositRequestValidator: AbstractValidator<DepositRequest>
    {
        public DepositRequestValidator() 
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма должна быть больше 0");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Укажите валюту")
                .Must(currency => new List<string>() { Currency.USD, Currency.GBP, Currency.GBP }.Contains(currency))
                .WithMessage("Укажите валюту");
        }


    }
}
