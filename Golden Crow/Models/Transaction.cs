namespace Golden_Crow.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int SenderAccountId { get; set; }

        public int ReceiverAccountId { get; set; }

        public DateTime MyProperty { get; set; }

        public decimal Amount { get; set; }

    }
}
