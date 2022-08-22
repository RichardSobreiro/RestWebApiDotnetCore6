using SimpleWebApi.Hateoas;

namespace SimpleWebApi.Domain.Base.Hateoas
{
    public interface ILinkedResource
    {
        IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}
