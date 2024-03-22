namespace User_Microservices.IService
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body);
    }
}
