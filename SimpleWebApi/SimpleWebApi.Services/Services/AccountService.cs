using AutoMapper;
using SimpleWebApi.Domain.Base.Exceptions;
using SimpleWebApi.Domain.Entities;
using SimpleWebApi.Domain.Repository;
using SimpleWebApi.Services.Interfaces;
using SimpleWebApi.Services.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace SimpleWebApi.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
        }

        public async Task<List<AccountViewModel>> GetAllAsync()
        {
            IEnumerable<Account> _accounts = await accountRepository.GetAllAsync();
            List<AccountViewModel> _accountsViewModels = mapper.Map<List<AccountViewModel>>(_accounts);

            throw new BusinessRuleException("Some generic exception");

            return _accountsViewModels;
        }

        public async Task<AccountViewModel> PostAsync(AccountViewModel accountViewModel)
        {
            if (accountViewModel.Id != Guid.Empty)
                throw new FieldValidationException("Account identifier must be empty");

            Validator.ValidateObject(accountViewModel, new ValidationContext(accountViewModel), true);

            Account _account = mapper.Map<Account>(accountViewModel);
            _account.Password = EncryptPassword(accountViewModel.Password);
            var model = await accountRepository.CreateAsync(_account);
            return mapper.Map<AccountViewModel>(model);
        }

        public async Task<AccountViewModel> GetByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid accountId))
                throw new FieldValidationException("Account identifier is not valid");

            Account _account = await accountRepository.GetAsync(x => x.Id == accountId && !x.IsDeleted);
            if (_account == null)
                throw new BusinessRuleException("Account not found");

            return mapper.Map<AccountViewModel>(_account);
        }

        public async Task<AccountViewModel> PutAsync(AccountViewModel accountViewModel)
        {
            if (accountViewModel.Id == Guid.Empty)
                throw new FieldValidationException("Account identifier is invalid");

            Account _account = await accountRepository.GetAsync(x => x.Id == accountViewModel.Id && !x.IsDeleted);
            if (_account == null)
                throw new BusinessRuleException("Account not found");

            _account = mapper.Map<Account>(accountViewModel);
            _account.Password = EncryptPassword(_account.Password);

            return mapper.Map<AccountViewModel>(await accountRepository.UpdateAsync(_account));
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
                throw new FieldValidationException("Account identifier is not valid");

            Account _account = await accountRepository.GetAsync(x => x.Id == userId && !x.IsDeleted);
            if (_account == null)
                throw new BusinessRuleException("Account not found");

            return await accountRepository.DeleteAsync(_account);
        }

        private string EncryptPassword(string password)
        {
            HashAlgorithm sha = SHA1.Create();

            byte[] encryptedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
