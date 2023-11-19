namespace CloudComputingUTN.DataAccessLayer
{
    public interface ILinkService
    {
        Link Generate(string endpointName, string action, object? routeValues, string rel, string method);
    }
}
