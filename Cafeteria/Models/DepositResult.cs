namespace Cafeteria.Models
{
    public class DepositResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public decimal BonusApplied { get; set; }
        public decimal NewBalance { get; set; }
    }
}
