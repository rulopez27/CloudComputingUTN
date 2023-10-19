namespace CloudComputingUTN.WebApp.DataAccessLayer.HATEOAS
{
    public class LinkCollectionWreapper<T>: DtoBase
    {
        public List<T> Value { get; set; } = new List<T>();

        public LinkCollectionWreapper()
        {
            
        }

        public LinkCollectionWreapper(List<T> value)
        {
            Value = value;
        }
    }
}
