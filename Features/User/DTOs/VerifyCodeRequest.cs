namespace TeloApi.Features.User.DTOs;

public class VerifyCodeRequest
{
    public string Email { get; set; }
    public string Code { get; set; }
}