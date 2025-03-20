namespace TeloApi.Features.User.Repositories;
using Models;
    
public interface IEmailVerificationRepository
{
    Task<EmailVerification?> GetVerificationByEmail(string email);
    Task SaveVerificationCode(EmailVerification emailVerification);
    Task<bool> IsEmailVerified(string email);
    Task MarkAsVerified(string email);
}