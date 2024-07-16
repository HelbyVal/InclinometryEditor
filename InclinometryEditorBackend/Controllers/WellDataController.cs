using InclinometryEditorBackend.Contracts;
using InclinometryEditorBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InclinometryEditorBackend.Controllers
{
    [Authorize]
    public class WellDataController : Controller
    {
        WellDataService _wellDataService = new WellDataService();

        [HttpPost("AddWellData")]
        public async Task<IActionResult> AddWellData(Guid wellId, double inclination, double azimut, double md)
        {
            var result = await _wellDataService.AddWellData(GetUser(), wellId, inclination, azimut, md);

            var response = result.ToResponse();

            return Ok(response);

        }

        [HttpGet("GetWellData")]
        public async Task<IActionResult> GetWellData(Guid wellId)
        {
            var res = await _wellDataService.GetWellData(GetUser(), wellId);
            var response = res.Select(x => x.ToResponse());

            return Ok(response);
        }

        [HttpDelete("DeleteWellData")]
        public async Task<IActionResult> DeleteWellData(Guid wellId)
        {
            return Ok(await _wellDataService.DeleteLastData(GetUser(), wellId));
        }

        string GetUser()
        {
            return User.Claims.ToList().Where(x => x.Type == "preferred_username").Select(x => x.Value).FirstOrDefault();
        }

    }
}
