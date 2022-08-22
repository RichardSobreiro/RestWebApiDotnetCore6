using SimpleWebApi.Domain.Base.Hateoas;

namespace SimpleWebApi.Hateoas
{
    public static class LinkedResourceExtension
    {
        public static void AddResourceLink(this ILinkedResource resources,
                  LinkedResourceType resourceType,
                  string routeUrl)
        {
            resources.Links ??= new Dictionary<LinkedResourceType, LinkedResource>();
            resources.Links[resourceType] = new LinkedResource(routeUrl);
        }
    }
}
