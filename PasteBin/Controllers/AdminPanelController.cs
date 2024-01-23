using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasteBin.Domain.DTOs;
using PasteBin.Domain.Model.RoleUsers;
using PasteBin.Services.Services;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("PasteBin/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminPanelController : ControllerBase
    {
        private readonly IUpdateUserRoleService _updateUserRoleService;

        public AdminPanelController(IUpdateUserRoleService updateUserRoleService)
        {
            _updateUserRoleService = updateUserRoleService;
        }

        [HttpPost]
        [Authorize(Roles = StaticRoleUsers.ADMIN)]
        [Route("UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateRole (UpdateRoleDto updateRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseUpdaterRole = await _updateUserRoleService.UpdateUserRole(updateRoleDto);

            if (responseUpdaterRole.StatusCode == 400)
                return BadRequest(responseUpdaterRole.Description);

            if (responseUpdaterRole.StatusCode == 404)
                return NotFound(responseUpdaterRole.Description);

            if(responseUpdaterRole.StatusCode == 500)
                return Problem(responseUpdaterRole.Description);

            return Ok(responseUpdaterRole);
        }
    }
}
