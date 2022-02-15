namespace Insurance.Common.Models.Bases
{
    public class ProcessResult<T>
    {
        public ProcessResult(string message)
        {
            ErrorMessage = message;
        }

        public ProcessResult(T resultObj)
        {
            ResultObject = resultObj;
        }

        public bool IsSuccess { get => string.IsNullOrEmpty(ErrorMessage); }
        public string ErrorMessage { get; set; }
        public T ResultObject { get; set; }
    }
}