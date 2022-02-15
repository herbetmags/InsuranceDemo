using Insurance.Common.Interfaces.Business;
using Insurance.Common.Models.AppSettings;
using Insurance.Common.Models.Bases;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Business.Managers
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IUsersDataManager _usersDataManager;
        private readonly string _key;
        public JwtAuthenticationManager(IUsersDataManager usersDataManager, IOptions<TokenSettings> token)
        {
            _usersDataManager = usersDataManager;
            _key = token.Value.JwtTokenKey;
        }

        public async Task<ProcessResult<AuthenticationToken>> GetAuthenticationTokenAsync(AuthenticationRequest request)
        {
            var processResult = new ProcessResult<AuthenticationToken>(string.Empty);
            try
            {
                var result = await _usersDataManager.GetUserByUserCredentialsAsync(request.Username, request.Password);
                var user = result.ResultObject;
                if (user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(_key);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, user.Username)
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials =
                            new SigningCredentials(
                                new SymmetricSecurityKey(tokenKey),
                                SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
                    var authToken = new AuthenticationToken { Token = tokenHandler.WriteToken(tokenObj) };
                    processResult = new ProcessResult<AuthenticationToken>(authToken);
                }
                else
                {
                    processResult= new ProcessResult<AuthenticationToken>("Either the username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<AuthenticationToken>(ex.Message);
            }
            return processResult;
        }
    }
}