using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Golden_Crow.DTOs.Finance
{
    public class TransactionHistoryRequest
    {

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        
        public int Limit { get; set; }

     
        public int Offset { get; set; }

    }
}
