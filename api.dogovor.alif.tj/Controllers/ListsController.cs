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

        [HttpGet("AgreementConcluderList")]
        public async Task<IActionResult> GetAgreementConcluderList()
        {
            return Ok(await _list.GetAgreementConcluder());
        }

        [HttpGet("CitizenshipList")]
        public async Task<IActionResult> CitizenshipList()
        {
            return Ok(await _list.GetCitizenship());
        }

        [HttpGet("CitiesList")]
        public async Task<IActionResult> CitiesList()
        {
            return Ok(await _list.GetCityList());
        }

        [HttpGet("PartialPaymentOrderList")]
        public async Task<IActionResult> PartialPaymentOrderList()
        {
            return Ok(await _list.GetPartialPaymentOrder());
        }

        [HttpGet("PassportTypesList")]
        public async Task<IActionResult> PassportTypesList()
        {
            return Ok(await _list.GetPassportTypes());
        }

        [HttpGet("PaymentOrdersList")]
        public async Task<IActionResult> GetPaymentOrdersList()
        {
            return Ok(await _list.GetPaymentOrder());
        }

        [HttpGet("RendedServicesVariationsList")]
        public async Task<IActionResult> GetRendedServicesVariationsList()
        {
            return Ok(await _list.GetRendedServicesVariations());
        }

        [HttpGet("ServiceList")]
        public async Task<IActionResult> GetServiceList()
        {
            return Ok(await _list.GetService());
        }

        [HttpGet("StructuralSubdivisionList")]
        public async Task<IActionResult> GetStructuralSubdivisionList()
        {
            return Ok(await _list.GetStructuralSubdivision());
        }

        [HttpGet("TrustieFoundationList")]
        public async Task<IActionResult> GetTrustieFoundationList()
        {
            return Ok(await _list.GetTrustieFoundation());
        }

        [HttpGet("ActVariationsOfCompletionsList")]
        public async Task<IActionResult> GetActVariationsofCompletionsList()
        {
            return Ok(await _list.GetActVariationsOfCompletions());
        }

        [HttpGet("AgreementEntitiesList")]
        public async Task<IActionResult> GetAgreementEntitiesList()
        {
            return Ok(await _list.GetAgreementEntities());
        }

    }
}
