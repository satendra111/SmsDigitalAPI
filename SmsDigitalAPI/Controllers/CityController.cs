using API.Model;
using AutoMapper;
using BAL.Abstract;
using Core.Concrete;
using Domain.Dto;
using Domain.EntityModel;
using Domain.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;
        private readonly IMapper mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            this.cityService = cityService;
            this.mapper = mapper;

        }
        // Post: api/<CityController>
        [HttpPost("GetCityAll")]
        public async Task<ActionResult> Get(CitySearchModel citySearchModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cityFilter = mapper.Map<CityDetailSearch>(citySearchModel);
            var item = await cityService.GetAllCityAsync(cityFilter);
            var response = new ApiResponseModel(item);
            return Ok(response);
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest();

            var item = await cityService.GetCityAsync(id);
            if(item == null)
                return  NotFound();

            var response = new ApiResponseModel(item);
            return Ok(response);
        }

        // POST api/<CityController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CityModel cityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = mapper.Map<CityDetailEntityModel>(cityModel);
            var item = await cityService.AddUpdateCityAsync(city);
          
            return CreatedAtAction("Get", new { id = item.Id }, item);
            
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CityModel cityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = mapper.Map<CityDetailEntityModel>(cityModel);
            city.Id = id;

            var item = await cityService.AddUpdateCityAsync(city);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id==0)
                return BadRequest();

            var existingItem =await cityService.GetCityAsync(id);
            if (existingItem == null)
            {
                return NoContent();
            }

            var item = await cityService.DeleteCityAsync(id);

            var response = new ApiResponseModel(item);
            return Ok(response);
        }
    }
}
