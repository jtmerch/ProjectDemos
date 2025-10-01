using System.Threading.Tasks;
using TSYSWasm.Models;

namespace TSYSWasm.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}