using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System;
using System.Threading.Tasks;

namespace LookatBackend.Services
{
    public class SmsService
    {
        private readonly string _accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        private readonly string _authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
        private readonly string _fromPhoneNumber = Environment.GetEnvironmentVariable("TWILIO_PHONE_NUMBER");

        public SmsService()
        {
            // Initialize Twilio client with the provided SID and Auth Token
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendOtpAsync(string toPhoneNumber, int otp)
        {
            try
            {
                // Ensure the phone numbers are in E.164 format
                var to = new PhoneNumber(FormatPhoneNumber(toPhoneNumber));  // Format the recipient's number
                var from = new PhoneNumber(FormatPhoneNumber(_fromPhoneNumber));  // Format your Twilio number

                var message = await MessageResource.CreateAsync(
                    to: to,
                    from: from,
                    body: $"Your verification code is {otp}");

                // Log the SID or any other response from Twilio for debugging
                Console.WriteLine($"SMS sent with SID: {message.Sid}");
            }
            catch (Twilio.Exceptions.ApiException apiEx)
            {
                Console.WriteLine($"Twilio API error: {apiEx.Message}");
                // Handle specific Twilio exceptions here if necessary
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                // Handle other errors (e.g., network issues)
            }
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            if (!phoneNumber.StartsWith("+"))
            {
                phoneNumber = "+" + phoneNumber;
            }
            return phoneNumber;
        }
    }
}
