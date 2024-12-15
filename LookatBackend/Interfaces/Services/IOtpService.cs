using LookatBackend.Dtos.User;

namespace LookatBackend.Interfaces.Services
{
    public interface IOtpService
    {

        Task<bool> IsEmailRegisteredAsync(string email);
        Task GenerateAndSendOtpAsync(string email);
        Task VerifyOtpAndRegisterUserAsync(VerifyOtpRequestDto verifyOtpRequest);
        Task<bool> VerifyOtpForPasswordChangeAsync(int otp);
    }
}
