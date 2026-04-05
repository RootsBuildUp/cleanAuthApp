namespace CleanAuthApp.Application.Common.Interfaces;

public interface IOtpService
{
    string GenerateOtp(string mobile);
    bool VerifyOtp(string mobile, string otp);
}