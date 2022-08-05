using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using crud_api.Models.Repositories;


namespace crud_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {

        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Get region from DB
            var walkDifficulty = await walkDifficultyRepository.DeleteAsync(id);

            //If null, not found
            if (walkDifficulty == null)
            {
                return NotFound();
            }

            //Convert response back to DTO

            var walkDifficultyDTO = mapper.Map<Models.DTO.Walk>(walkDifficulty);

            //return Ok response
            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Request to Domain model
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code,
            };

            //Pass details to repository
            var response = await walkDifficultyRepository.AddAsync(walkDifficulty);

            // Convert back to DTO
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Code = walkDifficulty.Code,
            };

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // Convert DTO to Domain model
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {

                Code = updateWalkDifficultyRequest.Code
            };
            //Update Walk difficulty using repository
            walkDifficulty = await walkDifficultyRepository.UpdateAsync(id, walkDifficulty);

            // If null, NOT FOUND
            if (walkDifficulty == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Code = walkDifficulty.Code
            };

            //Return Ok response
            return Ok(walkDifficultyDTO);

        }



        [HttpGet]
        public async Task<IActionResult> GetWalkDifficultyAsync()
        {
            var walksDifficultyDomain = await walkDifficultyRepository.GetAllAsync();

            var walksDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walksDifficultyDomain);

            return Ok(walksDifficultyDTO);
        }
    }
}