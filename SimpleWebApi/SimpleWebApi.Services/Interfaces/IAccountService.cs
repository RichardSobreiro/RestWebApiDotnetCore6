using SimpleWebApi.Services.ViewModels;

namespace SimpleWebApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountViewModel>> GetAllAsync();
        Task<AccountViewModel> PostAsync(AccountViewModel accountViewModel);
        Task<AccountViewModel> GetByIdAsync(string id);
        Task<AccountViewModel> PutAsync(AccountViewModel accountViewModel);
        Task<bool> DeleteAsync(string id);
    }
}
