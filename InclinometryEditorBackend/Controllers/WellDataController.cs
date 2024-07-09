using InclinometryEditorBackend.Contracts;
using InclinometryEditorBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace InclinometryEditorBackend.Controllers
{
    public class WellDataController : Controller
    {
        const int USER_ID = 1;
        WellDataService _wellDataService = new WellDataService();

        [HttpPost("AddWellData")]
        public async Task<IActionResult> AddWellData([FromBody] WellDataRequest request)
        {
            var result = await _wellDataService.AddWellData(USER_ID, request.WellId, request.Inclination, request.Azimut, request.Md);

            var response = result.ToResponse();

            return Ok(response);

        }

        [HttpGet("GetWellData")]
        public async Task<IActionResult> GetWellData(Guid wellId)
        {
            var res = await _wellDataService.GetWellData(USER_ID, wellId);
            var response = res.Select(x => x.ToResponse());

            return Ok(response);
        }

        [HttpDelete("DeleteWellData")]
        public async Task<IActionResult> DeleteWellData(Guid wellId)
        {
            return Ok(await _wellDataService.DeleteLastData(USER_ID, wellId));
        }

    }
}
