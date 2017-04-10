using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Data.EntityFramework;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Baldr.Models;

namespace Baldr.IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        protected Mock<IServiceProvider> ServiceProviderMock;
        protected EntityContextProvider EntityContextProvider;
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

            this.EntityContextProvider = new EntityContextProvider(this.Configuration.Object, this.HostingEnvironment.Object);

            this.UnitOfWork = new UnitOfWork(this.EntityContextProvider, this.ServiceProviderMock.Object);
        }


        [TestMethod]
        public void TestMethod1()
        {
            this.UnitOfWork.GetRepository<Institution>().Add(
                new Institution());

            this.UnitOfWork.Save();

            var inst = this.UnitOfWork.GetRepository<Institution>().Get(1);
        }


    }
}
