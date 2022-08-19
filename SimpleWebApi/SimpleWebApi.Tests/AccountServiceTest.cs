using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleWebApi.Domain.Base.Exceptions;
using SimpleWebApi.Domain.Entities;
using SimpleWebApi.Domain.Repository;
using SimpleWebApi.Services.AutoMapper;
using SimpleWebApi.Services.Interfaces;
using SimpleWebApi.Services.Services;
using SimpleWebApi.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SimpleWebApi.Tests
{
    [TestClass]
    public class AccountServiceTest
    {
        private readonly IAccountService accountService;

        public AccountServiceTest()
        {
            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);
            this.accountService = new AccountService(new Mock<IAccountRepository>().Object,_mapper);
        }

        [TestMethod]
        public async Task PostAsync_ValidateId_ThrowsException()
        {
            var exception = await Assert.ThrowsExceptionAsync<FieldValidationException>(
                () => accountService.PostAsync(new AccountViewModel() { Id = Guid.NewGuid() }));
            Assert.AreEqual("Account identifier must be empty", exception.Message);
        }

        [TestMethod]
        public async Task GetByIdAsync_SendingEmptyGuid_ThrowsException()
        {
            var exception = await Assert.ThrowsExceptionAsync<FieldValidationException>(
                () => accountService.GetByIdAsync(""));
            Assert.AreEqual("Account identifier is not valid", exception.Message);
        }

        [TestMethod]
        public async Task PutAsync_SendingEmptyGuid_ThrowsException()
        {
            var exception = await Assert.ThrowsExceptionAsync<FieldValidationException>(
                () => accountService.PutAsync(new AccountViewModel()));
            Assert.AreEqual("Account identifier is invalid", exception.Message);
        }

        [TestMethod]
        public async Task DeleteAsync_SendingEmptyGuid_ThrowsException()
        {
            var exception = await Assert.ThrowsExceptionAsync<FieldValidationException>(
                () => accountService.DeleteAsync(""));
            Assert.AreEqual("Account identifier is not valid", exception.Message);
        }

        [TestMethod]
        public async Task PostAsync_SendingValidObject_CreatedObject()
        {
            var account = new Account { Id = Guid.NewGuid(),
                DocumentNumber = "123", AccountType = "PP", Password = "123456" };
            var _accountRepository = new Mock<IAccountRepository>();
            _accountRepository.Setup(x => x.CreateAsync(It.IsAny<Account>())).ReturnsAsync(account);
            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);
            var accountService = new AccountService(_accountRepository.Object, _mapper);
            var result = await accountService.PostAsync(
                new AccountViewModel { DocumentNumber = "123", AccountType = "PP", Password = "123456" });
            Assert.IsInstanceOfType(result, typeof(AccountViewModel));
        }

        [TestMethod]
        public async Task GetAsync_ValidatingObject_ExistingObject()
        {
            List<Account> _accounts = new List<Account>();
            _accounts.Add(new Account { Id = Guid.NewGuid(), DocumentNumber = "123", 
                AccountType = "PP", Password = "123456", DateCreated = DateTime.Now });
            var _accountRepository = new Mock<IAccountRepository>();
            _accountRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(_accounts);
            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);
            var accountService = new AccountService(_accountRepository.Object, _mapper);
            var result = await accountService.GetAllAsync();
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task Post_SendingInvalidObject_ThrowsException()
        {
            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(
                () => accountService.PostAsync(new AccountViewModel { Password = "123456" }));
            Assert.AreEqual("The DocumentNumber field is required.", exception.Message);
        }
    }
}
