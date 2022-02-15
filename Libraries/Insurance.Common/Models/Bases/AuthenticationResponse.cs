namespace Insurance.Common.Models.Bases
{
    public class AuthenticationResponse : ResponseBase
    {
        public AuthenticationToken Token { get; set; }
    }
}