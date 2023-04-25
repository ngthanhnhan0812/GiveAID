namespace GiveAID.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email,string subject, string message);
    }
}
