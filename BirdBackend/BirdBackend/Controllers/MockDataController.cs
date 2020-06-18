using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdBackend.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BirdBackend.Controllers
{
    public class SerieDto {
        public string serie { get; set; }
        public int xValue { get; set; }
        public int yValue { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MockDataController : ControllerBase
    {

        [HttpPost]
        [Route("AddPoint")]
        public async Task<ActionResult> AddPoint(
            [FromServices] IHubContext<MockDataHub> mockDataHub,
            [FromBody] SerieDto serieDto
            )
        {
            await mockDataHub.Clients.All.SendAsync("AddedPoint", new { 
                serieDto.serie,
                serieDto.xValue,
                serieDto.yValue
            });
            return Ok();
        }

    }
}