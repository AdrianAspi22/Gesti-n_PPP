namespace GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Queries.GetAllAdvisoringContracts
{
    public class GetAllAdvisoringContractParameters
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int[] advisoringContractIds { get; set; }
        public string SearchString { get; set; } = "";
    }
}
