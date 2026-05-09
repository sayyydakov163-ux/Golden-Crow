using Golden_Crow.Database;
using Golden_Crow.Database.RabbitMQ;
using Golden_Crow.Models;
using Golden_Crow.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Golden_Crow.Features.Transfer
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, Result>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMessageProducer _messageProducer;

        public TransferCommandHandler(ApplicationDbContext context, IMessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
        }

        public async Task<Result> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            var fromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == request.FromUserId && a.Currency == request.Currency, cancellationToken);

            if (fromAccount == null)
            {
                return Result.Failure("Счет не найден");
            }

            var toUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.ToLogin, cancellationToken);

            if (toUser == null)
            {
                return Result.Failure("Получатель не найден");
            }

            var toAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == toUser.Id && a.Currency == request.Currency, cancellationToken);

            if (toAccount == null)
            {
                return Result.Failure("Счет получателя не найден");
            }

            if (fromAccount!.Balance < request.Amount)
            {
                return Result.Failure("Недостаточно средств");
            }

            fromAccount.Balance -= request.Amount;
            toAccount!.Balance += request.Amount;

            var transaction = new Transaction
            {
                ReceiverAccountId = toAccount.Id,
                SenderAccountId = fromAccount.Id,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow,
                Currency = request.Currency
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);

           await _messageProducer.SendMesageAsync(new TransactionCreatedEvent
            { 
                SenderId = request.FromUserId,
                ReceiverId = toUser.Id,
                Amount = transaction.Amount,
                Currency = transaction.Currency


            }, cancellationToken);


            return Result.Success();
        }


    }
}
