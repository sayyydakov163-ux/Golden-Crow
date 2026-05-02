using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Golden_Crow.DTOs.Finance
{
    public class TransferRequest
    {
        [FromQuery]
        [Required(ErrorMessage = "Поле token обязательно")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Поле ReceiverLogin  обязательно")]
        public string ReceiverLogin { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Поле Amount должно быть не менее 0.01")]
        public decimal Amount { get; set; }

    }
}
