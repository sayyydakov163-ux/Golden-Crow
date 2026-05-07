using FluentValidation;
using Golden_Crow.Attributes;
using Golden_Crow.DTOs.Finance;
using Golden_Crow.Models;
using Golden_Crow.Services.Finance;
using Microsoft.AspNetCore.Mvc;

namespace Golden_Crow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [MyAuthorize]
    public class FinanceController : ControllerBase
    {
       private readonly IFinanceService _financeService;

        public FinanceController(IFinanceService financeService)
        {

            _financeService = financeService;
        }

        [HttpGet("balance")]
        public async Task <IActionResult> GetBalance()
        {
            
            var result = await _financeService.GetBalanceAsync(GetUserId());
            if (result)
            {
                return Ok(new BalanceResponse { Balance = result.Value });
            }

            return BadRequest(new {Message = result.ErrorMessage });

            
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> DepositAsync([FromBody] DepositRequest request, [FromServices] IValidator<DepositRequest> validator)
        {

            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var depositResult = await _financeService.DepositAsync(GetUserId(), request.Amount);
            if (depositResult)
            { 
                return Ok();
            }

            return BadRequest(new { Message = depositResult.ErrorMessage });
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferAsync([FromBody] TransferRequest request, [FromServices] IValidator<TransferRequest> validator)
        {

            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var transferResult = await _financeService.TransferAsync(GetUserId(), request.ReceiverLogin, request.Amount);
            if (transferResult.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(new {Message = transferResult.ErrorMessage});



        }
     
        

        [HttpGet("history")]
        public async Task<IActionResult> GetTransactionHistoryAsync([FromQuery]TransactionHistoryRequest request, [FromServices] IValidator<TransactionHistoryRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var historyResult = await _financeService.GetTransactionHistoryAsync(GetUserId(), request.From, request.To, request.Offset, request.Limit);
            if (historyResult)
            {
                return Ok(historyResult.Value);
            }
            return BadRequest(new {Message = historyResult.ErrorMessage});

        }

        internal int GetUserId()
        {
            var userId = HttpContext.Items[Constants.UserIdContextParameter] as int?;
            return userId!.Value;

        }

    }
}
