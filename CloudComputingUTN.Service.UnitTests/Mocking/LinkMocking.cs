using CloudComputingUTN.DataAccessLayer;

namespace CloudComputingUTN.Service.UnitTests.Mocking
{
    public static class LinkMocking
    {
             
        public static Link CreateLink(string controller)
        {
            Link returnLink;
            returnLink = new Link($"{controller}1", "self", "GET");
            return returnLink;
        }
    }
}
