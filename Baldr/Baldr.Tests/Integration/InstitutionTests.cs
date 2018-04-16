using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Data.EntityFramework;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Baldr.Models;
using System.Collections.Generic;
using System.Linq;
using Baldr.Api.Tests.Integration;

namespace Baldr.IntegrationTests
{
    [TestClass]
    public class InstitutionTests
    {
        protected Mock<IServiceProvider> ServiceProviderMock;
        protected EntityContextProvider<BaldrDbContext> EntityContextProvider;
        protected IUnitOfWork UnitOfWork;
        protected Mock<IConfiguration> Configuration;
        protected Mock<IHostingEnvironment> HostingEnvironment;

        [TestInitialize]
        public void Setup()
        {
            this.ServiceProviderMock = new Mock<IServiceProvider>();
            this.ServiceProviderMock.Setup(sp => sp.GetService(typeof(IRepository<Institution>))).Returns(new Repository<Institution>());

            this.Configuration = new Mock<IConfiguration>();
            this.Configuration.Setup(c => c["ConnectionStrings:DefaultConnection"]).Returns("Data Source=Institution.db");

            this.HostingEnvironment = new Mock<IHostingEnvironment>();
            this.HostingEnvironment.Setup(h => h.EnvironmentName).Returns("IntegrationTesting");

            var dbContextFactory = new TestDbContextFactory(this.HostingEnvironment.Object, this.Configuration.Object);
            this.EntityContextProvider = new EntityContextProvider<BaldrDbContext>(this.Configuration.Object, this.HostingEnvironment.Object, dbContextFactory);

            this.UnitOfWork = new UnitOfWork(this.EntityContextProvider, this.ServiceProviderMock.Object);
        }


        [TestMethod]
        public void CreateInstitution()
        {
            var inst = new Institution {
                Id = 10,
                IsActive = true,
                Name = "Wells Fargo",
                ContactInfo = new Contact {
                    Address1 = "123 Easy St",
                    Address2 = "#321",
                    Address3 = "Bob Smith",
                    City = "Duluth",
                    State = "MN",
                    PostalCode = "12345-3233",
                    Country = "US",
                    Name = "Wells Fargo"
                }
            };

            this.UnitOfWork.GetRepository<Institution>().Add(inst);
            this.UnitOfWork.Save();

            var getInst = this.UnitOfWork.GetRepository<Institution>().Get(10);
            Assert.AreEqual(inst.Name, getInst.Name);
            Assert.AreEqual(inst.IsActive, getInst.IsActive);
            Assert.AreEqual(inst.ContactInfo.Address1, getInst.ContactInfo.Address1);
            Assert.AreEqual(inst.ContactInfo.Address2, getInst.ContactInfo.Address2);
            Assert.AreEqual(inst.ContactInfo.Address3, getInst.ContactInfo.Address3);
            Assert.AreEqual(inst.ContactInfo.City, getInst.ContactInfo.City);
            Assert.AreEqual(inst.ContactInfo.State, getInst.ContactInfo.State);
            Assert.AreEqual(inst.ContactInfo.PostalCode, getInst.ContactInfo.PostalCode);
            Assert.AreEqual(inst.ContactInfo.Country, getInst.ContactInfo.Country);
            Assert.AreEqual(inst.ContactInfo.Name, getInst.ContactInfo.Name);
        }

        [TestMethod]
        public void CreateInstitutionAccount()
        {
            var account = new Account {
                AccountNumber = "123",
                Comment = "A Comment.",
                InterestRate = 0.25M,
                Name = "Test Account",
                StartingBalance = 25.00M,
                Type = Models.Enums.AccountType.Bank,
                CurrentBalance = 45.00M
            };

            var inst = new Institution
            {
                Id = 10,
                IsActive = true,
                Name = "Wells Fargo",
                Accounts = new List<Account>
                {
                    account
                }
            };

            this.UnitOfWork.GetRepository<Institution>().Add(inst);
            this.UnitOfWork.Save();

            var getInst = this.UnitOfWork.GetRepository<Institution>().Get(10);
            Assert.AreEqual(inst.Name, getInst.Name);
            Assert.AreEqual(inst.IsActive, getInst.IsActive);

            var instAccount = inst.Accounts.First();
            var getInstAccount = getInst.Accounts.First();
            Assert.AreEqual(instAccount.AccountNumber, getInstAccount.AccountNumber);
            Assert.AreEqual(instAccount.Comment, getInstAccount.Comment);
            Assert.AreEqual(instAccount.InterestRate, getInstAccount.InterestRate);
            Assert.AreEqual(instAccount.Name, getInstAccount.Name);
            Assert.AreEqual(instAccount.StartingBalance, getInstAccount.StartingBalance);
            Assert.AreEqual(instAccount.Type, getInstAccount.Type);
            Assert.AreEqual(instAccount.CurrentBalance, getInstAccount.CurrentBalance);
        }
    }
}
