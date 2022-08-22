using SimpleWebApi.Domain.Base.Hateoas;
using SimpleWebApi.Hateoas;

namespace SimpleWebApi.Services.ViewModels
{
    public record AccountListResponseViewModel : ILinkedResource
    {
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }
        public List<AccountViewModel> Items { get; init; }

        public IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}
