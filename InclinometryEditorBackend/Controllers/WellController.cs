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
        WellService _wellService = new WellService();

        public WellController()
        { }

        [HttpPost("AddWell")]
        public async Task<IActionResult> AddWell(string title, string description, DateOnly createDate)
        {


            var result = await _wellService.AddWell(GetUser(), description, title, createDate);

            var response = new WellResponse(result.Id, result.Title, result.Description, result.CreateDate);

            return Ok(response);
        }

        [HttpGet("GetWells")]
        public async Task<ActionResult<List<WellResponse>>> GetWells()
        {

            
            var result = await _wellService.GetWells(GetUser());

            var res = result.Select(x => x.ToResponse());
            return Ok(res);
        }

        [HttpPatch("UpdateWell")]
        public async Task<IActionResult> UpdateWell(Guid wellId, string title, string description, DateOnly createDate)
        {
            var result = await _wellService.UpdateWell(GetUser(), wellId, title,
                                                                        description,
                                                                        createDate);
            var response = new WellResponse(result.Id, result.Title, result.Description, result.CreateDate);

            return Ok(response);
        }

        [HttpDelete("DeleteWell")]
        public async Task<IActionResult> DeleteWell(Guid wellId)
        {
            return Ok(await _wellService.DeleteWell(GetUser(), wellId));
        }

        string GetUser()
        {
            var e = User.Claims.ToList();
            return User.Claims.ToList().Where(x => x.Type == "preferred_username").Select(x => x.Value).FirstOrDefault();
        } 

    }
}
