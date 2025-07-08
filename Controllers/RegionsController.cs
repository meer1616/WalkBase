using Authentication.CustomActionFilters;
using Authentication.Data;
using Authentication.Models.Domain;
using Authentication.Models.DTO;
using Authentication.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;


//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class RegionsController : ControllerBase
    {
        //private readonly AuthenticationDBContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        private readonly ILogger<RegionsController> _logger;

        public RegionsController(
            ILogger<RegionsController> logger,
            AuthenticationDBContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper
            ) {
            //this.dbContext = dbContext;
            this.regionRepository= regionRepository;
            this.mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {

            //var regions = await dbContext.Regions.ToListAsync();
            var regionsDomain = await regionRepository.GetAllAsync();


            // Map Domain Models to DTOs
            //var regionDto = new List<RegionDto>();

            //foreach (var region in regions) {

            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}

            var regionDto=mapper.Map<List<RegionDto>>(regionsDomain);

            //var regions = new List<Region> {

            //    new Region
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="Ammeerr",
            //        Code="AMR",
            //        RegionImageUrl="www.google.com"
            //    }
            //};
            _logger.LogInformation($"Region controller log on :{DateTime.Now.TimeOfDay}");
            _logger.LogError(new Exception(), "---- Demo Exception ----");
            _logger.LogWarning("-*-*-*-* Demo Warning -*-*-*-*");
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id) {
            //var region=dbContext.Regions.Find(id);
            //var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            var regionDomain=await regionRepository.GetByIdAsync(id);

            if (regionDomain == null) {
                return NotFound();
            }

            // Convert/Map Domain Model to DTO


            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            _logger.LogInformation($"Region controller log on :{DateTime.Now.TimeOfDay}");
            _logger.LogInformation($"Count of Region controller :{regionDto}");

            return Ok(regionDto);
        }

        // [Authorize(Roles = "Writer")]
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionReqDto addregionDto) {


            // Convert/Map DTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = addregionDto.Code,
            //    Name = addregionDto.Name,
            //    RegionImageUrl = addregionDto.RegionImageUrl
            //};

            var regionDomainModel = mapper.Map<Region>(addregionDto);
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();
            regionDomainModel = await regionRepository.CreateRegionAsync(regionDomainModel);



            //Convert/Map Domain Model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById),new {id=regionDto.Id}, regionDto);
            
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //var regionDomainModel= await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //Convert/Map DTO to Domain Model

            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //regionDomainModel.Code=updateRegionRequestDto.Code;
            //regionDomainModel.Name=updateRegionRequestDto.Name;
            //regionDomainModel.RegionImageUrl=updateRegionRequestDto.RegionImageUrl;

            //await dbContext.SaveChangesAsync();

            // Convert/Map Domain Model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]    
        public async Task<IActionResult> Delete([FromRoute] Guid id) {

            var regionDomainModel = await regionRepository.DeleteRegionAsync(id);

            //var regionDomainModel= await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {

                return NotFound();
            }

            //dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            // Convert domain model to DTO

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }


}
