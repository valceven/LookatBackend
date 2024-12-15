using LookatBackend.Interfaces.Services;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security;
using LookatBackend.Dtos;
using LookatBackend.Dtos.User;


namespace LookatBackend.Services
{
    public class OtpService : IOtpService
    {
        private readonly LookatDbContext _context;
        private readonly EmailService _emailService;

        public OtpService(LookatDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task GenerateAndSendOtpAsync(string email)
        {
            // Generate OTP
            var otp = new Random().Next(1000, 10000);

            // Save OTP with expiration time
            var otpRecord = new OtpRecords
            {
                Email = email,
                Otp = otp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5)
            };

            _context.OtpRecords.Add(otpRecord);
            await _context.SaveChangesAsync();

            // Send OTP email
            var subject = "Your OTP Code";
            var body = $"Your OTP code is {otp}. It will expire in 5 minutes.";

            await _emailService.SendEmailAsync(email, subject, body);
        }

        public async Task VerifyOtpAndRegisterUserAsync(VerifyOtpRequestDto verifyOtpRequest)
        {
            var otpRecord = await _context.OtpRecords
                .FirstOrDefaultAsync(o => o.Email == verifyOtpRequest.Email && o.Otp == verifyOtpRequest.Otp);

            if (otpRecord == null)
                throw new Exception("Invalid OTP.");

            if (otpRecord.ExpirationTime < DateTime.UtcNow)
                throw new Exception("OTP has expired. Please request a new one.");

            otpRecord.ExpirationTime = DateTime.UtcNow;
            _context.OtpRecords.Update(otpRecord);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(verifyOtpRequest.Password);

            var user = new LookatBackend.Models.User
            {
                Email = verifyOtpRequest.Email,
                Password = hashedPassword,
           

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> VerifyOtpForPasswordChangeAsync(int otp)
        {
           
            var otpRecord = await _context.OtpRecords
                .FirstOrDefaultAsync(o => o.Otp == otp);

            if (otpRecord == null)
                return false; 

         
            if (otpRecord.ExpirationTime < DateTime.UtcNow)
                return false; 

  
            otpRecord.ExpirationTime = DateTime.UtcNow;
            _context.OtpRecords.Update(otpRecord);
            await _context.SaveChangesAsync(); 

            return true; 
        }

    }

}
