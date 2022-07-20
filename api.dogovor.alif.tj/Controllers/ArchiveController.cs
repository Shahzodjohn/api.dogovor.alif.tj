using Entity.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.UserService;
using System.Security.Claims;

namespace api.dogovor.alif.tj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        //private readonly IArchiveService _archive;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IUserService _service;

        //public ArchiveController(IArchiveService archive, IWebHostEnvironment webHostEnvironment, IUserService service)
        //{
        //    _archive = archive;
        //    _webHostEnvironment = webHostEnvironment;
        //    _service = service;
        //}
        //[HttpPost("ReturnFile")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<IActionResult> ReturnFile(ReturnFileDTO dto)
        //{
        //    var claim = User.Identity as ClaimsIdentity;
        //    if (claim == null) return BadRequest(System.Net.HttpStatusCode.BadRequest);
        //    var user = await _service.UsersInformation(claim);
        //    var path = (Path.Combine(_webHostEnvironment.WebRootPath, $"{ DateTime.Today.ToString("D") }"));
        //    var returnfilePath = await _archive.ReturnFile(dto, path, user);
        //    if (returnfilePath == null)
        //        return BadRequest("FilePath was returned as null!");
        //    var myfile = System.IO.File.ReadAllBytes(returnfilePath);
        //    return File(myfile, "application/octet-stream", dto.ContractName + $".{dto.format}");
        //}
    }
}
