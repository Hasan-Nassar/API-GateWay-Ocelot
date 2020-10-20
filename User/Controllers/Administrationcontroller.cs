using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Core.Dto;
using User.Services.Interface;

namespace User.Controllers
{
    [Route("User/[Controller]")]
    [ApiController]
    public class Administrationcontroller : ControllerBase
    {
        public IAdminstrationservice AdminstrationServices { get; }

        public Administrationcontroller(IAdminstrationservice adminstrationServices)
        {
            AdminstrationServices = adminstrationServices;
        }

        [HttpGet("Get-All")]
        public IActionResult Get() => Ok(AdminstrationServices.Get());

        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> Get(string id) => Ok(await AdminstrationServices.Get(id));

        [HttpPost("Create-Role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto) => Ok(await AdminstrationServices.CreateRole(createRoleDto));

        [HttpPut("Edit-Role")]
        public async Task<IActionResult> Put(string id, [FromBody] CreateRoleDto createRoleDto) => Ok(await AdminstrationServices.Put(id, createRoleDto));

        [HttpDelete("Delete-Role")]
        public async Task<IActionResult> Delete(string id) => Ok(await AdminstrationServices.Delete(id));
    }
}