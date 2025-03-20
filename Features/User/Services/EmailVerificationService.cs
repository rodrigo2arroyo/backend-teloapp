using TeloApi.Features.User.Repositories;
using TeloApi.Models;
using TeloApi.Templates;

namespace TeloApi.Features.User.Services;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IEmailVerificationRepository _emailVerificationRepository;
    private readonly IEmailService _emailService;

    public EmailVerificationService(IEmailVerificationRepository emailVerificationRepository, IEmailService emailService)
    {
        _emailVerificationRepository = emailVerificationRepository;
        _emailService = emailService;
    }

    public async Task<bool> SendVerificationCode(string email)
    {
        var random = new Random();
        string verificationCode = random.Next(100000, 999999).ToString();

        var emailVerification = new EmailVerification
        {
            Email = email,
            VerificationCode = verificationCode,
            ExpirationTime = DateTime.UtcNow.AddMinutes(10),
            Verified = false
        };

        await _emailVerificationRepository.SaveVerificationCode(emailVerification);

        // Leer la plantilla de correo y reemplazar el código dinámicamente
        string emailBody = EmailTemplate.VerificationCodeTemplate.Replace("{{CODE}}", verificationCode);

        await _emailService.SendEmailAsync(email, "Código de Verificación", emailBody, true);
        return true;
    }

    public async Task<bool> VerifyCode(string email, string code)
    {
        var verificationEntry = await _emailVerificationRepository.GetVerificationByEmail(email);

        if (verificationEntry == null || verificationEntry.VerificationCode != code || verificationEntry.ExpirationTime < DateTime.UtcNow)
        {
            return false;
        }

        await _emailVerificationRepository.MarkAsVerified(email);
        return true;
    }
}