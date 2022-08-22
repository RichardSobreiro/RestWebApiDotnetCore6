namespace SimpleWebApi.Services.ViewModels
{
    public record EntityViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
