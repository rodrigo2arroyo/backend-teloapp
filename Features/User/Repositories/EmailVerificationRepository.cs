using Microsoft.EntityFrameworkCore;
using TeloApi.Contexts;
using TeloApi.Models;

namespace TeloApi.Features.User.Repositories;
using Models;

public class EmailVerificationRepository(AppDbContext context) : IEmailVerificationRepository
{
    public async Task<EmailVerification?> GetVerificationByEmail(string email)
    {
        return await context.EmailVerifications
            .FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task SaveVerificationCode(EmailVerification emailVerification)
    {
        var existingEntry = await GetVerificationByEmail(emailVerification.Email);

        if (existingEntry != null)
        {
            existingEntry.VerificationCode = emailVerification.VerificationCode;
            existingEntry.ExpirationTime = emailVerification.ExpirationTime;
            existingEntry.Verified = false;
        }
        else
        {
            context.EmailVerifications.Add(emailVerification);
        }

        await context.SaveChangesAsync();
    }

    public async Task<bool> IsEmailVerified(string email)
    {
        var verification = await GetVerificationByEmail(email);
        return verification != null && verification.Verified.GetValueOrDefault();
    }


    public async Task MarkAsVerified(string email)
    {
        var verificationEntry = await GetVerificationByEmail(email);
        if (verificationEntry != null)
        {
            verificationEntry.Verified = true;
            await context.SaveChangesAsync();
        }
    }
}