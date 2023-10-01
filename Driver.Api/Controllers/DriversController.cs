using Drivers.Api.models;
using Drivers.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Drivers.Api.models;
namespace Drivers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly DriverService service;

        public DriversController(DriverService service) => this.service = service;

        [HttpGet]
        public async Task<ActionResult<List<Driver>>> Get() => await service.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Driver>> Get(string id)
        {
            var driver = await service.GetAsync(id);
            return driver is not null ? Ok(driver) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Driver driver)
        {
            await service.CreateAsync(driver);
            return CreatedAtAction(nameof(Get), driver.Id, driver);//the get method will work if the item was added
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, Driver driver)
        {
            var existingDriver = await service.GetAsync(id);
            if (existingDriver is null) return BadRequest();

            driver.Id = existingDriver.Id; //make sure that the id is the correct id 

            await service.UpdateAsync(id, driver);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingDriver = await service.GetAsync(id);
            if (existingDriver is null) return BadRequest();

            await service.RemoveAsync(id);
            return NoContent();
        }

    }
}
