using dotnet_app.Models.Dto;
using System.Threading.Tasks;

namespace dotnet_app.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
