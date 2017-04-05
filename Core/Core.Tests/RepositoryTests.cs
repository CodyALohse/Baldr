using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Core.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        protected Mock<IServiceProvider> ServiceProviderMock;
        protected FakeContextProvider FakeContextProvider;
        protected IUnitOfWork UnitOfWork;

        [TestInitialize]
        public void Setup()
        {
            this.ServiceProviderMock = new Mock<IServiceProvider>();
            this.ServiceProviderMock.Setup(sp => sp.GetService(typeof(IRepository<BaseModel>))).Returns(new Repository<BaseModel>());

            this.FakeContextProvider = new FakeContextProvider();

            this.UnitOfWork = new UnitOfWork(this.FakeContextProvider, this.ServiceProviderMock.Object);
        }

        [TestMethod]
        public void Test_AddEntity()
        {
            var baseModelGuid = Guid.NewGuid();

            var baseModel = new BaseModel
            {
                Id = baseModelGuid
            };

            this.FakeContextProvider.fakeContext.Clear();
            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            baseRepository.Add(baseModel);

            Assert.AreEqual(baseModel.Id, (this.FakeContextProvider.fakeContext.First(c => ((BaseModel)c).Id.Equals(baseModel.Id)) as BaseModel).Id);
        }

        [TestMethod]
        public void Test_GetEntityById()
        {
            var baseModelGuid = Guid.NewGuid();

            var baseModel = new BaseModel
            {
                Id = baseModelGuid
            };

            this.FakeContextProvider.fakeContext.Clear();
            this.FakeContextProvider.Add(baseModel);

            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            var getValue = baseRepository.Get(baseModel.Id);

            Assert.AreEqual(baseModel.Id, getValue.Id);
        }
    }
}