using CloudComputingUTN.WebApp.DataAccessLayer.HATEOAS;

namespace CloudComputingUTN.WebApp.DataAccessLayer
{
    public class DtoBase
    {
        public List<Link>  Links { get; set; } = new List<Link>();
    }
}
