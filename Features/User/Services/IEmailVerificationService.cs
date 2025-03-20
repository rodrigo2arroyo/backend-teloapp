namespace TeloApi.Features.User.Services;

public interface IEmailVerificationService
{
    Task<bool> SendVerificationCode(string email);
    Task<bool> VerifyCode(string email, string code);
}