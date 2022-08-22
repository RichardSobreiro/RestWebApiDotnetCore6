namespace SimpleWebApi.Hateoas
{
    public record LinkedResource(string Href);

    public enum LinkedResourceType
    {
        None,
        Prev,
        Next
    }
}
