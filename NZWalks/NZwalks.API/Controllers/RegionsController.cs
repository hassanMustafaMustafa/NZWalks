using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Repositories;
using AutoMapper;


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



    }
}
