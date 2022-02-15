using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.Bases;
using System;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class AuthenticationService : IJwtAuthenticationService
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public AuthenticationService(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public async Task<AuthenticationResponse> GetAuthenticationTokenAsync(AuthenticationRequest request)
        {
            var response = new AuthenticationResponse();
            try
            {
                var result = await _jwtAuthenticationManager.GetAuthenticationTokenAsync(request);
                if (result.IsSuccess)
                {
                    response.Token = result.ResultObject;
                }
                else
                {
                    response.ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}