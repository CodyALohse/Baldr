using Baldr.Modules.Institution.V1;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Baldr.Tests.Unit.Modules.Institution
{
    [TestClass]
    public class InstitutionV1Tests
    {
        protected Mock<IUnitOfWork> UnitOfWorkMock;
        protected Mock<IRepository<Models.Institution>> RepositoryMock;
        protected InstitutionController InstController;

        [TestInitialize]
        public void Setup()
        {
            this.RepositoryMock = new Mock<IRepository<Models.Institution>>();
            this.UnitOfWorkMock = new Mock<IUnitOfWork>();
            this.UnitOfWorkMock.Setup(uow => uow.GetRepository<Models.Institution>()).Returns(this.RepositoryMock.Object);

            this.InstController = new InstitutionController(this.UnitOfWorkMock.Object);
        }

        [TestMethod]
        public async Task GetInstitution_NoMatchingId_Response404()
        {
            var result = await this.InstController.Get(1);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetInstitution_MatchingId_Response200()
        {
            var institutionMock = new Mock<Models.Institution>();
            this.RepositoryMock.Setup(r => r.Get(1)).Returns(institutionMock.Object);

            var result = await this.InstController.Get(1);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
