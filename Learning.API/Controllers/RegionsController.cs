using AutoMapper;
using Learning.API.CustomeActionFilters;
using Learning.API.Data;
using Learning.API.Models.Domain;
using Learning.API.Models.DTO;
using Learning.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Text.Json;

namespace Learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly LerningDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;
        #region constructor 

        public RegionsController(LerningDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        #endregion
        [HttpGet]
        //JWT token
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            #region DTOs wise data pass
            //Get Data from Database - Domain models
            //var regionsDomain = await dbContext.Regions.ToListAsync();

            //logger
            try
            {
                //repository wise
                var regionsDomain = await regionRepository.GetAllAsync();
                //Mapper

               // logger.LogInformation($"Finished GetAllRegions request with data:{JsonSerializer.Serialize(regionsDomain)}");
                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

                //Map Domain models to DTOs
                //var regionsDto = new List<RegionDto>();
                //foreach (var regionDomain in regionsDomain)
                //{
                //    regionsDto.Add(new RegionDto()
                //    {
                //        Id = regionDomain.Id,
                //        Code = regionDomain.Code,
                //        Name = regionDomain.Name,
                //        RegionImageUrl = regionDomain.RegionImageUrl

                //    });
                //}

                //Return DTOs
                //return Ok(regionsDto);
                #endregion

                #region Simple get data
                //var regions = dbContext.Regions.ToList();
                //return Ok(regions);
                #endregion

                #region hard code data passing in the region tables
                //var regions = new List<Region>
                //{
                //    new Region {
                //        Id = Guid.NewGuid(),
                //        Name = "Auckland regions",
                //        Code = "AKL",
                //        RegionImageUrl =""
                //    }
                //};
                #endregion
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

        }
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            #region Linq query wise pass the data
            //Linq method wise pass
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            //if (region == null)
            //{
            //    return NotFound();
            //}
            //return Ok(region);
            #endregion

            #region Get region Domain Model From database
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/convert the Region Domain model to region DTOs
            //var regionsDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            ////Return DTO back to client 
            //return Ok(regionsDto);

            //Mapper
            return Ok(mapper.Map<RegionDto>(regionDomain));
            #endregion
        }

        #region Post To create new Region
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            //Map or convert DTO to domain model
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //};

            //Mapper
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create Region
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            //repository wise pass the data
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map domain model back to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            //Mapper
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);


        }
        #endregion


        #region update region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] updateregionrequestDto updateregionrequestDto)
        {


            //Map DTO to domian model
            //var regionDomainModel = new Region
            //{
            //    Code = updateregionrequestDto.Code,
            //    Name = updateregionrequestDto.Name,
            //    RegionImageUrl = updateregionrequestDto.RegionImageUrl
            //};

            //Mapper
            var regionDomainModel = mapper.Map<Region>(updateregionrequestDto);

            //check if region exists
            //var regionDomianModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            ////repository wise pass the data
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Map DTo to Domain Model
            //regionDomianModel.Code = updateregionrequestDto.Code;
            //regionDomianModel.Name = updateregionrequestDto.Name;
            //regionDomianModel.RegionImageUrl = updateregionrequestDto.RegionImageUrl;
            //await dbContext.SaveChangesAsync();

            //Interface wise pass the code
            //regionDomainModel.Code = updateregionrequestDto.Code;
            //regionDomainModel.Name = updateregionrequestDto.Name;
            //regionDomainModel.RegionImageUrl = updateregionrequestDto.RegionImageUrl;
            //await dbContext.SaveChangesAsync();

            //Convert model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            //Mapper
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

        }
        #endregion

        #region Delete Method
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //repository wise pass the data
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();

            }

            //Delete region
            //dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            //return deleted Region back
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            //return Ok(regionDto);

            //mapper wise pass the data
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
        #endregion
    }
}
