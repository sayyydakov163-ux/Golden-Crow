using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Golden_Crow.DTOs.Finance
{
    public class DepositRequest
    {
       

        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
