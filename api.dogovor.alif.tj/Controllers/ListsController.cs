using IDropDownListService;
using Microsoft.AspNetCore.Mvc;

namespace api.dogovor.alif.tj.Controllers
{
    public class ListsController : ControllerBase
    {
        private readonly IDropDowns _list;

        public ListsController(IDropDowns list)
        {
            _list = list;
        }

        [HttpGet("AgentList")]
        public async Task<IActionResult> GetAgentList()
        {
            return Ok(await _list.GetAgent());
        }

        [HttpGet("Agreement Concluder List")]
        public async Task<IActionResult> GetAgreementConcluderList()
        {
            return Ok(await _list.GetAgreementConcluder());
        }

        [HttpGet("Citizenship List")]
        public async Task<IActionResult> CitizenshipList()
        {
            return Ok(await _list.GetCitizenship());
        }

        [HttpGet("Cities List")]
        public async Task<IActionResult> CitiesList()
        {
            return Ok(await _list.GetCityList());
        }

        [HttpGet("Partial Payment Order List")]
        public async Task<IActionResult> PartialPaymentOrderList()
        {
            return Ok(await _list.GetPartialPaymentOrder());
        }

        [HttpGet("Passport Types List")]
        public async Task<IActionResult> PassportTypesList()
        {
            return Ok(await _list.GetPassportTypes());
        }

        [HttpGet("Payment Orders List")]
        public async Task<IActionResult> GetPaymentOrdersList()
        {
            return Ok(await _list.GetPaymentOrder());
        }

        [HttpGet("Rended Services Variations List")]
        public async Task<IActionResult> GetRendedServicesVariationsList()
        {
            return Ok(await _list.GetRendedServicesVariations());
        }

        [HttpGet("Service List")]
        public async Task<IActionResult> GetServiceList()
        {
            return Ok(await _list.GetService());
        }

        [HttpGet("Structural Subdivision List")]
        public async Task<IActionResult> GetStructuralSubdivisionList()
        {
            return Ok(await _list.GetStructuralSubdivision());
        }
        [HttpGet("Trustie Foundation List")]
        public async Task<IActionResult> GetTrustieFoundationList()
        {
            return Ok(await _list.GetTrustieFoundation());
        }
        [HttpGet("Act Variations of Completions List")]
        public async Task<IActionResult> GetActVariationsofCompletionsList()
        {
            return Ok(await _list.GetActVariationsOfCompletions());
        }
        [HttpGet("Agreement Entities List")]
        public async Task<IActionResult> GetAgreementEntitiesList()
        {
            return Ok(await _list.GetAgreementEntities());
        }

    }
}
