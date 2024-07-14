using InclinometryEditorBackend.Contracts;
using InclinometryEditorBackend.Models;
using InclinometryEditorBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InclinometryEditorBackend.Controllers
{

    [Authorize]
    public class WellController : Controller
    {
        const int USER_ID = 1;
        WellService _wellService = new WellService();
        [HttpPost("AddWell")]
        public async Task<IActionResult> AddWell(string title, string description, DateOnly createDate)
        {

            var result = await _wellService.AddWell(USER_ID, description, title, createDate);

            var response = new WellResponse(result.Id, result.Title, result.Description, result.CreateDate);

            return Ok(response);
        }

        [HttpGet("GetWells")]
        public async Task<ActionResult<List<WellResponse>>> GetWells()
        {

            var r = Request.Headers;
            var z = Request;
            var u = User;
            var result = await _wellService.GetWells(USER_ID);

            var res = result.Select(x => x.ToResponse());
            return Ok(res);
        }

        [HttpPatch("UpdateWell")]
        public async Task<IActionResult> UpdateWell(Guid wellId, string title, string description, DateOnly createDate)
        {
            var result = await _wellService.UpdateWell(USER_ID, wellId, title,
                                                                        description,
                                                                        createDate);
            var response = new WellResponse(result.Id, result.Title, result.Description, result.CreateDate);

            return Ok(response);
        }

        [HttpDelete("DeleteWell")]
        public async Task<IActionResult> DeleteWell(Guid wellId)
        {
            return Ok(await _wellService.DeleteWell(USER_ID, wellId));
        }

    }
}
