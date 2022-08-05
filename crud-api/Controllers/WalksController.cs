using Microsoft.AspNetCore.Mvc;
using crud_api.Models.Domain;
using AutoMapper;
using crud_api.Models.Repositories;

namespace crud_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository)
        {
            this.walkRepository = walkRepository;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }



    [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Validate the request
            if (!await ValidateUpdateWalkAsync(updateWalkRequest))
            {
                return BadRequest(ModelState);
            }
            // Convert DTO to Domain model
            var walk = new Models.Domain.Walk()
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };
            //Update Walk using repository
            walk = await walkRepository.UpdateAsync(id, walk);

            // If null, NOT FOUND
            if (walk == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Length = walk.Length,
                Name = walk.Name,
                WalkDifficultyId = walk.WalkDifficultyId,
                RegionId = walk.RegionId
            };

            //Return Ok response
            return Ok(walkDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Validate the request
            if (!(await ValidateAddWalkAsync(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }


            //Request to Domain model
            var walk = new Models.Domain.Walk()
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            //Pass details to repository
            var response = await walkRepository.AddAsync(walk);

            // Convert back to DTO
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Length = walk.Length,
                Name = walk.Name,
                RegionId = walk.RegionId,
            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);

        }

        [HttpGet]
        public async Task<IActionResult> GetWalksAsync()
        {

            var walksDomain = await walkRepository.GetAllAsync();

            
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walkDomain = await walkRepository.GetAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Get region from DB
            var walk = await walkRepository.DeleteAsync(id);

            //If null, not found
            if (walk == null)
            {
                return NotFound();
            }

            //Convert response back to DTO

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            //return Ok response
            return Ok(walkDTO);
        }

        #region
        private async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            var region = await regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                        $"{nameof(updateWalkRequest.RegionId)} is invalid");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region
        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            var region = await regionRepository.GetAsync(addWalkRequest.RegionId);
            if(region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                        $"{nameof(addWalkRequest.RegionId)} is invalid");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }

}