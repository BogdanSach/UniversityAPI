using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Data;
using UniversityAPI.Repositories.UniBuildingRepos;
using UniversityAPI.Models;
using UniversityAPI.Models.DTO.UniBuildingDto;
using System.Runtime.CompilerServices;
using UniversityAPI.Models.Domain;

namespace UniversityAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class UniversityBuildingController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUniversityBuildingRepository uniBuildingRepository;

        public UniversityBuildingController(IMapper mapper, IUniversityBuildingRepository universityBuildingRepository)
        {
            this.mapper = mapper;
            this.uniBuildingRepository = universityBuildingRepository;
        }

        // Action method to get all university buildings
        // GET: api/UniversityBuildings
        [HttpGet]
        [Route("UniversityBuildings")]
        public async Task<ActionResult<List<UniversityBuildingDto>>> GetAllUniBuildings()
        {
            var uniBuildingDomain = await uniBuildingRepository.GetAllAsync();

            var uniBuildingDto = mapper.Map<List<UniversityBuildingDto>>(uniBuildingDomain);
            return Ok(uniBuildingDto);
        }

        // Action method to get university building by id
        // GET: api/UniversityBuildings/{id}
        [HttpGet]
        [Route("UniversityBuildings/{id:guid}")]
        public async Task<ActionResult<UniversityBuildingDto>> GetById([FromRoute] Guid id)
        {
            var uniBuildingDomain = await uniBuildingRepository.GetByIdAsync(id);

            if (uniBuildingDomain == null) { return NotFound(); }

            var uniBuildingDto = mapper.Map<UniversityBuildingDto>(uniBuildingDomain);
            return Ok(uniBuildingDto);
        }

        // Action method to get all university buildings for specific university
        // GET: api/Universities/{universityId}/UniversityBuildings
        [HttpGet]
        [Route("Universities/{universityId:guid}/UniversityBuildings")]
        public async Task<ActionResult<List<UniversityBuildingDto>>> GetAllUniBuildingsForUni([FromRoute] Guid universityId)
        {
            var uniBuildingDomain = await uniBuildingRepository.GetByUniversityIdAsync(universityId);
            if (uniBuildingDomain == null) { return NotFound(); }

            var uniBuildingDto = mapper.Map<List<UniversityBuildingDto>>(uniBuildingDomain);

            return Ok(uniBuildingDto);
        }

        // Action method to get university building by id for specific university
        // GET: api/Universities/{universityId}/UniversityBuildings/{id}
        [HttpGet]
        [Route("Universities/{universityId:guid}/UniversityBuildings/{id:guid}")]
        public async Task<ActionResult<UniversityBuildingDto>> GetUniBuildingByIdForUni([FromRoute] Guid universityId, [FromRoute] Guid id)
        {
            var uniBuildingDomain = await uniBuildingRepository.GetByIdForUniAsync(universityId, id);

            if (uniBuildingDomain == null) { return NotFound(); }

            var uniBuildingDto = mapper.Map<UniversityBuildingDto>(uniBuildingDomain);

            return Ok(uniBuildingDto);
        }

        // Action method to create new university building
        // POST: api/Universities/{universityId}/UniversityBuildings
        [HttpPost]
        [Route("Universities/{universityId:guid}/UniversityBuildings")]
        public async Task<IActionResult> CreateUniBuilding([FromRoute] Guid universityId, [FromBody] AddUniversityBuildingRequestDto addUniversityBuildingRequestDto)
        {
            var uniBuildingDomain = mapper.Map<UniversityBuilding>(addUniversityBuildingRequestDto);
            uniBuildingDomain.UniversityId = universityId;

            uniBuildingDomain = await uniBuildingRepository.CreateAsync(uniBuildingDomain);

            var uniBuildingDto = mapper.Map<UniversityBuildingDto>(uniBuildingDomain);
            return CreatedAtAction(nameof(GetById), new { id = uniBuildingDomain.Id}, uniBuildingDto.Id );
        }

        // Action method to update information about existing university
        // PUT: api/Universities/{universityId}/UniversityBuildings/{id}
        [HttpPut]
        [Route("Universities/{universityId:guid}/UniversityBuildings/{id:guid}")]
        public async Task<IActionResult> UpdateUniBuilding([FromRoute] Guid universityId, [FromRoute] Guid id, [FromBody] UpdateUniversityBuildingRequestDto updateUniversityBuildingRequestDto)
        {
            var uniBuildingDomain = mapper.Map<UniversityBuilding>(updateUniversityBuildingRequestDto);
            uniBuildingDomain.Id = id;  
            uniBuildingDomain.UniversityId = universityId;

            uniBuildingDomain = await uniBuildingRepository.UpdateAsync(universityId, id, uniBuildingDomain);
            if (uniBuildingDomain == null) { return NotFound(); }

            var uniBuildingDto = mapper.Map<UniversityBuildingDto>(uniBuildingDomain);

            return Ok(uniBuildingDto);
        }

        // Action method to delete existing university building
        // DELETE: api/Universities/{universityId}/UniversityBuildings/{id}
        [HttpDelete]
        [Route("Universities/{universityId:guid}/UniversityBuildings/{id:guid}")]
        public async Task<IActionResult> DeleteUniBuilding([FromRoute] Guid universityId, [FromRoute] Guid id)
        {
            var uniBuildingDomain = await uniBuildingRepository.DeleteAsync(universityId, id);
            if (uniBuildingDomain == null) { return NotFound(); }

            return NoContent();
        }
    }
}
