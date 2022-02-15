namespace Insurance.Common.Models.Bases
{
    public class ResponseBase
    {
        public bool IsSuccess { get => string.IsNullOrEmpty(ErrorMessage); }
        public string ErrorMessage { get; set; }
    }
}