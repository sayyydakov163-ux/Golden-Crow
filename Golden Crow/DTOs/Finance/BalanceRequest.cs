using Microsoft.AspNetCore.Mvc;

namespace Golden_Crow.DTOs.Finance
{
    public class BalanceRequest
    {
        [FromQuery] public string Currency { get; set; }
    }
}
