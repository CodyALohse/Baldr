using AutoMapper;
using Baldr.Modules.Institution.V1.ApiModels;

namespace Baldr.Modules.Institution.V1.ApiMappers
{
    public class InstitutionMapper : Profile
    {
        public InstitutionMapper()
        {
            CreateMap<Models.Institution, InstitutionResult>();
        }
    }
}
