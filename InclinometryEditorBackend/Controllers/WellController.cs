using InclinometryEditorBackend.Contracts;
using InclinometryEditorBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace InclinometryEditorBackend.Controllers
{

    public class WellController : Controller
    {
        const int USER_ID = 1;
        WellService _wellService = new WellService();
        [HttpPost("AddWell")]
        public async Task<IActionResult> AddWell([FromBody] WellRequest request)
        {
            var result = await _wellService.AddWell(USER_ID, request.Discription, request.Title, request.CreateDate);

            var response = new WellResponse(result.Id, result.Title, result.Description, result.CreateDate.ToString());

            return Ok(response);
        }
    }
}
