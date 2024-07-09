using InclinometryEditorBackend.Contracts;
using InclinometryEditorBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace InclinometryEditorBackend.Controllers
{
    public class WellDataController : Controller
    {
        const int USER_ID = 1;
        WellDataService _wellDataService = new WellDataService();

        [HttpPatch("AddWellData")]
        public async Task<IActionResult> AddWellData([FromBody] WellDataRequest request)
        {
            var result = await _wellDataService.AddWellData(USER_ID, request.WellId, request.Inclination, request.Azimut, request.Md);

            var response = result.ToResponse();

            return Ok(response);

        }
    }
}
