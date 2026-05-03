using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Golden_Crow.DTOs.Finance
{
    public class DepositRequest
    {
       

        [Range(0.01, double.MaxValue, ErrorMessage ="Поле Amount должно быть не менее 0.01")]
        public decimal Amount { get; set; }
    }
}
