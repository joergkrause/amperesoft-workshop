using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WorkshopSolution.BusinessLogic;
using WorkshopSolution.DataTransferObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkshopSolution.BackendServiceCtrl.Controllers
{
  [Authorize(Policy = "RackUser")]
  [Route("api/[controller]")]
  [ApiController]
  [Produces("application/json")]
  [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
  public class RackController : ControllerBase
  {

    private readonly IRackManager _rackManager;

    public RackController(IRackManager rackManager)
    {
      _rackManager = rackManager;
    }

    [HttpGet(Name = "GetAllRacks")]
    [ProducesResponseType(typeof(IEnumerable<RackListDto>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
      var data = _rackManager.GetRacks();     
      return Ok(data);
    }

    [HttpGet("{id}", Name = "GetRack")]
    [ProducesResponseType(typeof(RackDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]    
    public IActionResult Get(int id)
    {
      var data = _rackManager.GetRack(id);
      if (data == null)
      {
        return NotFound(); // 404
      }
      return Ok(data); // 200
    }

    [HttpPost(Name = "AddRack")]
    [ProducesResponseType(typeof(RackDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] RackDetailDto value)
    {      
      return Created();
    }

    [HttpPut("{id}", Name = "UpdateRack")]
    [ProducesResponseType(typeof(RackDetailDto), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public IActionResult Put(int id, [FromBody] string value)
    {
      return Accepted();
    }

    [Authorize(Policy = "DeleteRackUser")] // richtig. vorher prüfen!
    [HttpDelete("{id}", Name = "DeleteRack")]
    [ProducesResponseType(typeof(RackDetailDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
      // User.IsInRole("user"); // zu spät!
      return NoContent();
    }

  }
}
