using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Golden_Crow.DTOs.Finance
{
    public class TransactionHistoryRequest
    {

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Поле limit должно быть не меньше 1")]
        public int Limit { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Поле offset не может быть отрицательным")]
        public int Offset { get; set; }

    }
}
