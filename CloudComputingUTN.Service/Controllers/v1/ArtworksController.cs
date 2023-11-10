using AutoMapper;
using CloudComputingUTN.DataAccessLayer;
using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudComputingUTN.Service.Controllers.v1
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
        public async Task<IActionResult> Get(int id, LinkGenerator linkGenerator)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var artwork = await museumDbRepository.GetArtworkById(id);
                var artworkDto = _mapper.Map<ArtworkDto>(artwork);
                artworkDto.CreateArtworkLinks(linkGenerator, _contextAccessor);
                return Ok(artworkDto);
            }
            catch (InvalidOperationException ioex)
            {
                if (ioex.Message == "Sequence contains no elements.")
                {
                    return NotFound();
                }
                return StatusCode(500, ioex.Message);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }

        // POST api/<ArtworksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Artwork value, LinkGenerator linkGenerator)
        {
            try
            {
                HttpContext context = Request.HttpContext;

                await museumDbRepository.CreateArtwork(value);
                string uri = linkGenerator.GetUriByAction(context, "Get", "Artworks", new { id = value.ArtworkId });

                return Created(uri, value);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }

        // PUT api/<ArtworksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Artwork value, LinkGenerator linkGenerator)
        {
            try
            {
                HttpContext context = Request.HttpContext;

                await museumDbRepository.UpdateArtwork(value);
                return Ok(value);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }

        // DELETE api/<ArtworksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpContext context = Request.HttpContext;
                bool deleted = await museumDbRepository.DeleteArtwork(id);
                return Ok("Artwork deleted");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(500, errorMessage);

            }
        }
    }
}
