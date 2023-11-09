using AutoMapper;
using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.WebApp.DataAccessLayer;
using CloudComputingUTN.WebApp.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudComputingUTN.WebApp.api.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private IMuseumDbRepository museumDbRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;

        public ArtworksController(IMuseumDbRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            museumDbRepository = repository;
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
        }


        // GET: api/<ArtworksController>
        [HttpGet]
        public async Task<IActionResult> Get(LinkGenerator linkGenerator)
        {
            try
            {
                var artworks = await museumDbRepository.GetArtworks();
                var artworksDtoList = new List<ArtworkDto>();
                foreach (var artwork in artworks)
                {
                    ArtworkDto artworkDto = _mapper.Map<ArtworkDto>(artwork);
                    artworkDto.CreateArtworkLinks(linkGenerator, _contextAccessor);
                    artworksDtoList.Add(artworkDto);
                }
                return Ok(artworksDtoList);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }

        // GET api/<ArtworksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ArtworksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArtworksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArtworksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
