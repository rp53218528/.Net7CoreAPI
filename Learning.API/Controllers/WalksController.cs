using AutoMapper;
using Learning.API.Data;
using Learning.API.Models.Domain;
using Learning.API.Models.DTO;
using Learning.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;

namespace Learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        #region Create Walk 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddwalkRequestDto addwalkRequestDto)
        {
            //Map DTO to  Domain Model
            var walkDomainModel = mapper.Map<Walk>(addwalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);

            //Map domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
        #endregion

        #region GetData
        [HttpGet]
        //GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
           [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber, [FromQuery] int pageSize = 1000)
        {

            //try
            //{
            //    throw new Exception("this was the error");

            //    var walkDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
            //  pageNumber, pageSize);
            //    //Map Domain Model to Dto
            //    return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));
            //}
            //catch (Exception)
            //{
            //    //Log this exception
            //    return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            //    throw;
            //}

            //------Global Exception
            var walkDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);
            //Create an exception
            throw new Exception("this is a exception");
            //Map Domain Model to Dto
            return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));


        }
        #endregion

        #region GetdataById
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
        #endregion

        #region UpdateData
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            try
            {
                //Map Domain Model to Dto
                var WalkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                WalkDomainModel = await walkRepository.updateAsync(id, WalkDomainModel);
                if (WalkDomainModel == null)
                {
                    return NotFound();
                }
                //map domain model to Dto
                return Ok(mapper.Map<WalkDto>(WalkDomainModel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DeleteData
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteWalkDomianModel = await walkRepository.deleteasync(id);
            if (deleteWalkDomianModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deleteWalkDomianModel));
        }
        #endregion
    }
}

