namespace LookatBackend.Dtos.User
{
    public class VerifyOtpRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
    }
}