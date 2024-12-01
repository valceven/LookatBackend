using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using DotNetEnv;

namespace TwilioApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioController : ControllerBase
    {
        public TwilioController()
        {
            // Load environment variables from .env file
            Env.Load();

            // Initialize Twilio client
            TwilioClient.Init(
                Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
                Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
            );
        }

        [HttpPost("send-sms")]
        public IActionResult SendSms([FromBody] SmsRequest request)
        {
            var message = MessageResource.Create(
                body: request.Message,
                from: new PhoneNumber(Environment.GetEnvironmentVariable("TWILIO_PHONE_NUMBER")),
                to: new PhoneNumber(request.ToPhoneNumber)
            );

            return Ok(new { message.Sid });
        }
    }

    public class SmsRequest
    {
        public string ToPhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
