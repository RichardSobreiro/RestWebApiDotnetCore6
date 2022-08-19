using AutoMapper;
using SimpleWebApi.Domain.Entities;
using SimpleWebApi.Services.ViewModels;

namespace SimpleWebApi.Services.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            #region ViewModelToDomain

            CreateMap<AccountViewModel, Account>();

            #endregion

            #region DomainToViewModel

            CreateMap<Account, AccountViewModel>();

            #endregion
        }
    }
}
