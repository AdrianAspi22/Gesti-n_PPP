namespace GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetAllAdvisoringRequests
{
    public class GetAllAdvisoringRequestParameters
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int[] advisoringRequestIds { get; set; }
        public string SearchString { get; set; } = "";
    }
}
