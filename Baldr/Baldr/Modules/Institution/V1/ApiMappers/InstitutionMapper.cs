using AutoMapper;
using Baldr.Modules.Institution.V1.ApiModels;

namespace Baldr.Modules.Institution.V1.ApiMappers
{
    /// <summary>
    /// Automapper registers any mapper classes that extend the Profile class
    /// </summary>
    public class InstitutionMapper : Profile
    {
        public InstitutionMapper()
        {
            CreateMap<Models.Institution, InstitutionResult>();
        }
    }
}
