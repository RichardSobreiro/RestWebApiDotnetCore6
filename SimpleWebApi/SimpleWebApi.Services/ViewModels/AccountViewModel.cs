using System.ComponentModel.DataAnnotations;

namespace SimpleWebApi.Services.ViewModels
{
    public record AccountViewModel : EntityViewModel
    {
        [Required]
        public string DocumentNumber { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
