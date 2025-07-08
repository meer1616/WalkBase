using Authentication.CustomActionFilters;
using Authentication.Models.Domain;
using Authentication.Models.DTO;
using Authentication.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {

        private readonly IWalkRepository walkRepository;

        private readonly IMapper mapper;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map Dto to Domain Model
            var walkDoaminModel = mapper.Map<Walk>(addWalkRequestDto);

            var walkRespDto = await walkRepository.CreateAsync(walkDoaminModel);

            return Ok(walkRespDto);


        }

        [HttpGet]
        [Authorize(Roles = "Reader")]   
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            // Get all walks from the repository
            var walksDomainModel = await walkRepository.GetAllWalkAsync(filterOn, filterQuery,sortBy,isAscending ?? true, pageNumber,pageSize);
            // Map Domain Models to DTOs
            var walkDto = mapper.Map<List<WalkDto>>(walksDomainModel);
            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            // Get walk by ID from the repository
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain Model to DTO
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkReqDto updateWalkReqDto)
        {
            // Validate the request
           
            // Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkReqDto);
            // Update walk in the repository
            var updatedWalk = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (updatedWalk == null)
            {
                return NotFound();
            }
            // Map updated Domain Model to DTO
            var updatedWalkDto = mapper.Map<WalkDto>(updatedWalk);
            return Ok(updatedWalkDto);
          
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            // Delete walk from the repository
            var deletedWalk = await walkRepository.DeleteAsync(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }
            // Map deleted Domain Model to DTO
            var deletedWalkDto = mapper.Map<WalkDto>(deletedWalk);
            return Ok(deletedWalkDto);
        }
    }
}
