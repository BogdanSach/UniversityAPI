using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Data;
using UniversityAPI.Repositories;
using UniversityAPI.Models.Domain;
using UniversityAPI.Models.DTO;
using AutoMapper;
using UniversityAPI.Models.DTO.UniDTOs;
using UniversityAPI.Models.DTO.DormDTOs;
using UniversityAPI.Repositories.DormRepos;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly UniDbContext uniDbContext;
        private readonly IUniversityRepository universityRepository;
        private readonly IMapper mapper;
        private readonly IDormRepository dormRepository;

        public UniversitiesController(UniDbContext uniDbContext, IUniversityRepository universityRepository, IMapper mapper, IDormRepository dormRepository)
        {
            this.uniDbContext = uniDbContext;
            this.universityRepository = universityRepository;
            this.mapper = mapper;
            this.dormRepository = dormRepository;
        }

        // Action method to get all universities
        // GET: api/Universities
        [HttpGet]
        public async Task<ActionResult> GetAllUniversities()
        {
            var universityDomain = await universityRepository.GetAllAsync();

            var universityDto = mapper.Map<List<UniversityDto>>(universityDomain);
            return Ok(universityDto);
        }

        // Action method to get a university by ID
        // GET: api/Universities/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingUniversity = await universityRepository.GetByIdAsync(id);
            if (existingUniversity == null)
            {
                return NotFound();
            }

            var universityDto = mapper.Map<UniversityDto>(existingUniversity);
            return Ok(universityDto);
        }

        // Actions method to get list of dorms for specific university
        // GET api/Universities/{id}/Dorms
        [HttpGet]
        [Route("{id:guid}/Dorms")]
        public async Task<ActionResult<List<DormDto>>> GetDormsForUniversity([FromRoute] Guid id)
        {
            var dormDomain = await dormRepository.GetByUniversityIdAsync(id);

            if (dormDomain  == null || !dormDomain.Any())
            {
                return NotFound();
            }

            var dormDto = mapper.Map<List<DormDto>>(dormDomain);
            return Ok(dormDto);
        }

        // Action method to create a new university
        // POST: api/Universities
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUniversityRequestDto addUniversityRequestDto)
        {
            var universityDomainModel = mapper.Map<University>(addUniversityRequestDto);

            universityDomainModel = await universityRepository.CreateAsync(universityDomainModel);

            var universityDto = mapper.Map<UniversityDto>(universityDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = universityDomainModel.Id }, universityDto);
        }

        // Action method to update an existing university
        // PUT: api/Universities/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUniversityRequestDto updateUniversityRequestDto)
        {
            var universityDomainModel = mapper.Map<University>(updateUniversityRequestDto);

            universityDomainModel = await universityRepository.UpdateAsync(id, universityDomainModel);
            if (universityDomainModel == null)
            {
                return NotFound();
            }


            var universityDto = mapper.Map<UniversityDto>(universityDomainModel);
            return Ok(universityDto);

        }

        // Action method to delete a university
        // DELETE: api/Universities/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var universityDomainModel = await universityRepository.DeleteAsync(id);
            if (universityDomainModel == null)
            {
                return NotFound();
            }

            var universityDto = mapper.Map<UniversityDto>(universityDomainModel);
            return Ok(universityDto);
        }
    }
}
