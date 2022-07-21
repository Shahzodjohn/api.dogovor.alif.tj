using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDropDownLists
    {
        public Task<List<City>> GetCityList(); 
        public Task<List<StructuralSubdivision>> GetStructuralSubdivision(); 
        public Task<List<TrustieFoundation>> GetTrustieFoundation(); 
        public Task<List<Citizenship>> GetCitizenship(); 
        public Task<List<PassportType>> GetPassportType(); 
        public Task<List<AgreementConcluder>> GetAgreementConcluder(); 
        public Task<List<Services>> GetService(); 
        public Task<List<PaymentOrder>> GetPaymentOrder(); 
        public Task<List<PartialPaymentOrder>> GetPartialPaymentOrder(); 
        public Task<List<Agent>> GetAgent(); 
        public Task<List<RendedServicesVariations>> GetRendedServicesVariations(); 
        public Task<List<ActVariationsOfCompletion>> GetActVariationsOfCompletions(); 
        public Task<List<AgreementEntity>> GetAgreementEntities(); 
    }
}
