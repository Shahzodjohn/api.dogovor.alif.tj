using IDropDownListService;
using Microsoft.AspNetCore.Mvc;

namespace api.dogovor.alif.tj.Controllers
{
    public class ListsController : ControllerBase
    {
        private readonly IDropDownListServices _list;

        public ListsController(IDropDownListServices list)
        {
            _list = list;
        }

        [HttpGet("GetAgentList  ")]
        public async Task<IActionResult> GetAgentList()
        {
            return Ok(await _list.GetAgent());
        }

        [HttpGet("GetAgreementConcluderList")]
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

        [HttpGet("GetPaymentOrdersList")]
        public async Task<IActionResult> GetPaymentOrdersList()
        {
            return Ok(await _list.GetPaymentOrder());
        }

        [HttpGet("GetRendedServicesVariationsList")]
        public async Task<IActionResult> GetRendedServicesVariationsList()
        {
            return Ok(await _list.GetRendedServicesVariations());
        }

        [HttpGet("GetServiceList")]
        public async Task<IActionResult> GetServiceList()
        {
            return Ok(await _list.GetService());
        }

        [HttpGet("GetStructuralSubdivisionList")]
        public async Task<IActionResult> GetStructuralSubdivisionList()
        {
            return Ok(await _list.GetStructuralSubdivision());
        }
        [HttpGet("GetTrustieFoundationList")]
        public async Task<IActionResult> GetTrustieFoundationList()
        {
            return Ok(await _list.GetTrustieFoundation());
        }
    }
}
