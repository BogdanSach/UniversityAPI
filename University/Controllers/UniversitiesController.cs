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
        private readonly IUniversityRepository universityRepository;
        private readonly IMapper mapper;

        public UniversitiesController(IUniversityRepository universityRepository, IMapper mapper)
        {
            this.universityRepository = universityRepository;
            this.mapper = mapper;
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
