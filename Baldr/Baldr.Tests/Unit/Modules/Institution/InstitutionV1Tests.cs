using AutoMapper;
using Baldr.Modules.Institution.V1;
using Baldr.Modules.Institution.V1.ApiModels;
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
        protected Mock<IMapper> Mapper;
        protected InstitutionController InstController;

        [TestInitialize]
        public void Setup()
        {
            this.RepositoryMock = new Mock<IRepository<Models.Institution>>();
            this.UnitOfWorkMock = new Mock<IUnitOfWork>();
            this.UnitOfWorkMock.Setup(uow => uow.GetRepository<Models.Institution>()).Returns(this.RepositoryMock.Object);

            this.Mapper = new Mock<IMapper>();
            this.InstController = new InstitutionController(this.UnitOfWorkMock.Object, this.Mapper.Object);
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

        [TestMethod]
        public async Task GetInstitution_ValidModelResultMapping_Response200()
        {
            var institution = new Models.Institution
            {
                Name = "FakeInstitution",
                Id = 1,
                IsActive = true,
                CreatedOn = System.DateTimeOffset.MinValue,
                ModifiedOn = System.DateTimeOffset.MaxValue
            };

            var institutionResult = new InstitutionResult
            {
                Name = institution.Name,
                IsActive = institution.IsActive,
            };

            this.RepositoryMock.Setup(r => r.Get(1)).Returns(institution);
            this.Mapper.Setup(m => m.Map<InstitutionResult>(institution)).Returns(institutionResult);

            var result = await this.InstController.Get(1);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
