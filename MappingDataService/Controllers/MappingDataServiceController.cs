using MappingDataService.MappingDataDAL;
using MappingDataService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MappingDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappingDataServiceController : ControllerBase
    {
        private readonly SqlData _SqlData;
        public MappingDataServiceController(SqlData sqlData)
        {
            _SqlData = sqlData;
        }

        [HttpPost("GetPosByIslemId")]
        public async Task<IActionResult> postPos([FromBody] int _IslemId)
        {
            try
            {
                List<MapData> list = await _SqlData.runSp(_IslemId);

                return Ok(list);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
 
        }

    }
}
