using LookatBackend.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace LookatBackend.Interfaces.Services
{
    public interface ILoginService
    {
        Task<IActionResult> LoginAsync(UserLoginDto loginRequest);
    }
}
