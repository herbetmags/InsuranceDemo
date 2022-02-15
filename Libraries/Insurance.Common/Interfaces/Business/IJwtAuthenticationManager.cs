using Insurance.Common.Models.Bases;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Business
{
    public interface IJwtAuthenticationManager
    {
        Task<ProcessResult<AuthenticationToken>> GetAuthenticationTokenAsync(AuthenticationRequest request);
    }
}