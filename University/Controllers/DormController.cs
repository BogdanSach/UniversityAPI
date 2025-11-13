using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Data;
using UniversityAPI.Models.Domain;
using UniversityAPI.Models.DTO.DormDTOs;
using UniversityAPI.Repositories.DormRepos;

namespace UniversityAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class DormController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDormRepository dormRepository;

        public DormController(IMapper mapper, IDormRepository dormRepository)
        {
            this.mapper = mapper;
            this.dormRepository = dormRepository;
        }

        // Action method to get all dorms
        // GET: api/Dorms
        [HttpGet]
        [Route("Dorms")]
        public async Task<IActionResult> GetAllDorms()
        {
            var dormDomain = await dormRepository.GetAllAsync();

            var dormDto = mapper.Map<List<DormDto>>(dormDomain);
            return Ok(dormDto);
        }

        // Action method to get specific dorm by ID
        // GET: api/Dorms/{id}
        [HttpGet]
        [Route("/Dorms/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingDorm = await dormRepository.GetByIdAsync(id);

            if (existingDorm == null)
            {
                return NotFound();
            }

            var dormDto = mapper.Map<DormDto>(existingDorm);
            return Ok(dormDto);
        }


        // Action method to get list of dorms for specific university
        // GET api/Universities/{id}/Dorms
        [HttpGet]
        [Route("Universities/{universityId:guid}/Dorms")]
        public async Task<ActionResult<List<DormDto>>> GetDormsForUniversity([FromRoute] Guid universityId)
        {
            var dormDomain = await dormRepository.GetByUniversityIdAsync(universityId);

            if (dormDomain == null || !dormDomain.Any())
            {
                return NotFound();
            }

            var dormDto = mapper.Map<List<DormDto>>(dormDomain);
            return Ok(dormDto);
        }

        // Action method to get a dorm by id for specific university
        // GET api/Universities/{universityId}/Dorms/{id
        [HttpGet]
        [Route("Universities/{universityId:guid}/Dorms/{id:guid}")]
        public async Task<IActionResult> GetDormByIdForUni([FromRoute] Guid universityId, [FromRoute] Guid id)
        {
            var dormDomain = await dormRepository.GetByIdForUniAsync(universityId, id);

            if (dormDomain == null)
            {
                return NotFound();
            }
            var dormDto = mapper.Map<DormDto>(dormDomain);
            return Ok(dormDto);
        }

        // Action method to create a new Dorm for a specific university
        // POST: api/Universities/{id}/Dorms
        [HttpPost]
        [Route("Universities/{universityId:guid}/Dorms")]
        public async Task<IActionResult> CreateDorm([FromBody] AddDormRequestDto addDormRequestDto, [FromRoute] Guid universityId)
        {
            var dormDomainModel = mapper.Map<Dorm>(addDormRequestDto);
            dormDomainModel.UniversityId = universityId;

            dormDomainModel = await dormRepository.CreateAsync(dormDomainModel);

            var dormDto = mapper.Map<DormDto>(dormDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = dormDomainModel.Id }, dormDto);
        }

        //Action method to update information about an existing dorm
        // PUT: api/Universities/{universityId}/Dorms/{id}
        [HttpPut]
        [Route("Universities/{universityId:guid}/Dorms/{id:guid}")]
        public async Task<IActionResult> UpdateDorm([FromRoute] Guid universityId, [FromRoute] Guid id, [FromBody] UpdateDormRequestDto updateDormRequestDto)
        {
            var dormDomainModel = mapper.Map<Dorm>(updateDormRequestDto);
            dormDomainModel.UniversityId = universityId;

            dormDomainModel = await dormRepository.UpdateAsync(universityId, id, dormDomainModel);
            if (dormDomainModel == null)
            {
                return NotFound();
            }

            var dormDto = mapper.Map<DormDto>(dormDomainModel);
            return Ok(dormDto);
        }

        // Action method to delete an existing dorm
        // DELETE: api/Universities/{universityId}/Dorms/{id}
        [HttpDelete]
        [Route("Universities/{universityId:guid}/Dorms/{id:guid}")]
        public async Task<IActionResult> DeleteDorm([FromRoute] Guid universityId, [FromRoute] Guid id)
        {
            var dormDomainModel = await dormRepository.DeleteAsync(universityId, id);

            if (dormDomainModel == null)
            {
                return NotFound();
            }

            var dormDto = mapper.Map<DormDto>(dormDomainModel);
            return NoContent();
        }

    }
}