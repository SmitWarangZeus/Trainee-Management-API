namespace TraineeManagement.api.Models
{
    public class PagedResponse<TraineeResponse>
    {
        public List<TraineeResponse> Data { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        
        public int TotalRecords { get; set; }

        public PagedResponse(List<TraineeResponse> data, int pageNumber, int pageSize, int totalRecords)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Data = data;
        }
    }
}