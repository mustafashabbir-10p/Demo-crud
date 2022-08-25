using Demo.models;
using Demo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class PController : ControllerBase
    {
        private readonly IPersonData _personData;
        public PController(IPersonData pdata)
        {
            _personData = pdata;
        }

        [HttpGet]
        
        public async Task<IActionResult> getPersons()
        {
            return Ok(await _personData.getPersons());
        }

        [HttpPost]
        public async Task<IActionResult> addPerson([FromBody] Person add)
        {
            return Ok(await _personData.addPerson(add));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> updateperson([FromBody] Person upd, [FromRoute] Guid id)
        {
            return Ok(await _personData.updatePerson(upd, id));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> getPerson([FromRoute] Guid id)
        {
            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(_personData.verifyUser(user, id)){
                return Ok(await _personData.getPerson(id));
            }
            return BadRequest("Access revoked");
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> deletePerson([FromRoute] Guid id)
        {
            return Ok(await _personData.deletePerson(id));
        }

    }
}
