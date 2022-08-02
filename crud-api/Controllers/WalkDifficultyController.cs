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

            var walkDifficultyDTO = mapper.Map<Models.DTO.Walk>(walkDifficultyDomain);

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



        [HttpGet]
        public async Task<IActionResult> GetWalkDifficultyAsync()
        {
            var walksDifficultyDomain = await walkDifficultyRepository.GetAllAsync();

            var walksDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walksDifficultyDomain);

            return Ok(walksDifficultyDTO);
        }
    }
}