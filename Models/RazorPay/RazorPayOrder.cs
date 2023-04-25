namespace GiveAID.Models.RazorPay
{
    public class RazorPayOrder
    {
        public string? OrderId { get; set; }
        public string? RazorPayAPIKey { get; set; }
        public int Amount { get; set; }
        public string? Currency { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
