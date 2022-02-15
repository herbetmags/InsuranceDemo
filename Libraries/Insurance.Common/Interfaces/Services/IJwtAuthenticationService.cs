using Insurance.Common.Models.Bases;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Services
{
    public interface IJwtAuthenticationService
    {
        Task<AuthenticationResponse> GetAuthenticationTokenAsync(AuthenticationRequest request);
    }
}