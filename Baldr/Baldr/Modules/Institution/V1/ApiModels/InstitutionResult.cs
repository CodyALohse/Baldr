using Baldr.Models;
using Core.Api;

namespace Baldr.Modules.Institution.V1.ApiModels
{
    public class InstitutionResult : BaseResult
    {
        public string Name { get; set; }

        public Contact ContactInfo { get; set; }

        public bool IsActive { get; set; }
    }
}
