namespace Insurance.Common.Models.Bases
{
    public class SearchResponse<T> : ResponseBase where T : class
    {
        public int TotalRecords { get; set; }
        public T Records { get; set; }
    }
}