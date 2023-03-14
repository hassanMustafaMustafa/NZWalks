using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Repositories;
using AutoMapper;
using NZwalks.API.Models.DTO;


namespace NZwalks.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    
    public class RegionsController : Controller
    {
        private readonly RegionRepository regionRepository;

        private IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = (RegionRepository?)regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {

            var regions = await regionRepository.GetAllAsync();

            //Return DTO regions

            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    { 
            //        Id= region.Id,

            //        Name = region.Name,
            //        code = region.code,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long= region.Long,
            //        Population = region.Population

            //    };
            //    regionsDTO.Add(regionDTO);

            //});

           var regionsDTO= mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regions);


        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetReguonAsync")]
        public async Task<IActionResult> GetReguonAsync(Guid id)
        {

           var region= await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }

           var regionDTO=  mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);


        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // Request To Domain model
            var region = new Models.Domain.Region()
            {
                code = addRegionRequest.code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            //Pass Details To Repository
           region = await regionRepository.AddAsync(region);
            // Convert back To DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                code = region.code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetReguonAsync), new { id = regionDTO.Id, regionDTO });
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAysnc(Guid id)
        {
            // Get the region from DB
           var region= await regionRepository.DeleteAsync(id);

            // If Null Notfound
            if (region == null)
            {
                return NotFound();
            }
            //Convert Response back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                code = region.code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };
            //return Ok response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody]Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain model
            var region = new Models.Domain.Region()
            {
                code = updateRegionRequest.code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population

            };


            //update Region using Repository
          region= await regionRepository.UpdateAsync(id,region);



            //if null then notfound
            if (region ==null)
            {
                return NotFound();
            }
            // covert Domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                code = region.code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //Return Ok response

            return Ok(regionDTO);

        }
    }
}
