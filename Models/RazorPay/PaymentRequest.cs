using System.ComponentModel.DataAnnotations;

namespace GiveAID.Models.RazorPay
{
    public class PaymentRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
