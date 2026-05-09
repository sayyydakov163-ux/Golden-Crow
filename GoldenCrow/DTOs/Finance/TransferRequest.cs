using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Golden_Crow.DTOs.Finance
{
    public class TransferRequest
    {
       
        public string Currency { get; set; }
       
        public string ReceiverLogin { get; set; }

      
        public decimal Amount { get; set; }

    }
}
