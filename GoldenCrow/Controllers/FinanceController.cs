using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Golden_Crow.Attributes;
using Golden_Crow.DTOs.Finance;
using Golden_Crow.Features.Deposit;
using Golden_Crow.Features.GetBalance;
using Golden_Crow.Features.GetTransactionHistory;
using Golden_Crow.Features.Transfer;
using Golden_Crow.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Golden_Crow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [MyAuthorize]
    public class FinanceController : ControllerBase
    {
      
       private readonly IMediator _mediator;

        public FinanceController(IMediator mediator)
        {

            
            _mediator = mediator;
        }

        [HttpGet("balance")]
        public async Task <IActionResult> GetBalanceAsync(BalanceRequest request, IValidator<BalanceRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var balanceResult = await _mediator.Send(new GetBalanceQuery(GetUserId(), request.Currency));
            if (balanceResult)
            {
                return Ok(new BalanceResponse { Balance = balanceResult.Value });
            }

            return BadRequest(new {Message = balanceResult.ErrorMessage });

            
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> DepositAsync([FromBody] DepositRequest request, [FromServices] IValidator<DepositRequest> validator)
        {

            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(new DepositCommand(GetUserId(), request.Amount, request.Currency));
           

            if (result)
            {
                return Ok();
            }
            return BadRequest(new { Message = result.ErrorMessage });
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferAsync([FromBody] TransferRequest request, [FromServices] IValidator<TransferRequest> validator)
        {

            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(new TransferCommand(GetUserId(), request.ReceiverLogin, request.Amount, request.Currency));
           

            if (result)
            {
                return Ok();
            }
            return BadRequest(new {Message = result.ErrorMessage});



        }
     
        

        [HttpGet("history")]
        public async Task<IActionResult> GetTransactionHistoryAsync([FromQuery]TransactionHistoryRequest request, [FromServices] IValidator<TransactionHistoryRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var result = await _mediator.Send(new GetTransactionHistoryQuery(GetUserId(), request.From, request.To, request.Offset, request.Limit));
            
            if (result)
            {
                return Ok(result.Value);
            }
            return BadRequest(new {Message = result.ErrorMessage});

        }

        internal int GetUserId()
        {
            var userId = HttpContext.Items[Constants.UserIdContextParameter] as int?;
            return userId!.Value;

        }

    }
}
