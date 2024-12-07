namespace LookatBackend.Dtos.User
{
    public class VerifyOtpRequestDto
    {
        public UserDto UserDto { get; set; }
        public int Otp { get; set; }  
    }
}
