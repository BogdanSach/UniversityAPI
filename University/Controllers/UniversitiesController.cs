using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Data;
using UniversityAPI.Repositories;
using UniversityAPI.Models.Domain;
using UniversityAPI.Models.DTO;
using AutoMapper;
using UniversityAPI.Models.DTO.UniDTOs;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly UniDbContext uniDbContext;
        private readonly IUniversityRepository universityRepository;
        private readonly Mapper mapper;

        public UniversitiesController(UniDbContext uniDbContext, IUniversityRepository universityRepository, Mapper mapper)
        {
            this.uniDbContext = uniDbContext;
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
            await uniDbContext.SaveChangesAsync();

            var universityDto = mapper.Map<UniversityDto>(universityDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = universityDto.Id }, universityDto);
        }

        // Action method to update an existing university
        // PUT: api/Universities/{id}
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] updateUniversityRequestDto updateUniversityRequestDto)
        {
            var universityDomainModel = mapper.Map<University>(updateUniversityRequestDto);

            universityDomainModel = await universityRepository.UpdateAsync(id, universityDomainModel);
            if (universityDomainModel == null)
            {
                return NotFound();
            }
            universityDomainModel.Name = updateUniversityRequestDto.Name;
            universityDomainModel.Address = updateUniversityRequestDto.Address;
            universityDomainModel.Url = updateUniversityRequestDto.Url;
            universityDomainModel.Description = updateUniversityRequestDto.Description;

            await uniDbContext.SaveChangesAsync();

            var universityDto = mapper.Map<UniversityDto>(universityDomainModel);
            return Ok(universityDto);

        }

        // Action method to delete a university
        // DELETE: api/Universities/{id}
        [HttpDelete]
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
