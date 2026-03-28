namespace Golden_Crow.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public DateTime MyProperty { get; set; }

        public decimal Amount { get; set; }

    }
}
