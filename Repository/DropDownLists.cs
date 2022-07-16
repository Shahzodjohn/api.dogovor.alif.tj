using ConnectionProvider.Context;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DropDownLists : IDropDownLists
    {
        private readonly AppDbСontext _context;

        public DropDownLists(AppDbСontext context)
        {
            _context = context;
        }

        public async Task<List<Agent>> GetAgent()
        {
            return await _context.Agent.ToListAsync();
        }

        public async Task<List<AgreementConcluder>> GetAgreementConcluder()
        {
            return await _context.AgreementConcluder.ToListAsync();
        }

        public async Task<List<Citizenship>> GetCitizenship()
        {
           return await _context.Citizenship.ToListAsync();
        }

        public async Task<List<City>> GetCityList()
        {
            return await _context.City.ToListAsync();
        }

        public async Task<List<PassportType>> GetPassportType()
        {
            return await _context.PassportType.ToListAsync();
        }

        public async Task<List<PartialPaymentOrder>> GetPartialPaymentOrder()
        {
            return await _context.PartialPaymentOrderName.ToListAsync();
        }

        public async Task<List<PaymentOrder>> GetPaymentOrder()
        {
            return await _context.PaymentOrder.ToListAsync();
        }

        public async Task<List<RendedServicesVariations>> GetRendedServicesVariations()
        {
            return await _context.RendedServicesVariations.ToListAsync();
        }

        public async Task<List<Services>> GetService()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<List<StructuralSubdivision>> GetStructuralSubdivision()
        {
            return await _context.StructuralSubdivision.ToListAsync();
        }

        public async Task<List<TrustieFoundation>> GetTrustieFoundation()
        {
            return await _context.TrustieFoundation.ToListAsync();
        }
    }
}
