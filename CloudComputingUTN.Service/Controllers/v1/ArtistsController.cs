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
    public class ArtistsController : ControllerBase
    {
        private IMuseumDbRepository museumDbRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;
        private ILinkService _linkService;
        public ArtistsController(IMuseumDbRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILinkService linkService)
        {
            museumDbRepository = repository;
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
            _linkService = linkService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(LinkGenerator linkGenerator)
        {
            try
            {
                var artists = await museumDbRepository.GetArtists();
                List<ArtistDto> artistsDtos = new List<ArtistDto>();
                foreach (var artist in artists)
                {
                    var artistDto = _mapper.Map<ArtistDto>(artist);
                    artistDto.CreateArtistLinks(_linkService, linkGenerator, _contextAccessor);
                    artistsDtos.Add(artistDto);
                }
                return Ok(artistsDtos);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }

        // GET api/<ArtistsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, LinkGenerator linkGenerator)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var artist = await museumDbRepository.GetArtistById(id);
                var artistDto = _mapper.Map<ArtistDto>(artist);
                artistDto.CreateArtistLinks(_linkService, linkGenerator, _contextAccessor);

                return Ok(artistDto);
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

        // POST api/<ArtistsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Artist value, LinkGenerator linkGenerator)
        {
            try
            {
                await museumDbRepository.CreateArtist(value);
                ArtistDto artistDto = _mapper.Map<ArtistDto>(value);
                artistDto.CreateArtistLinks(_linkService, linkGenerator, _contextAccessor);
                string uri = "";
                if (artistDto.Links.Any())
                {
                    uri = artistDto.Links.First(link => link.Rel == "self").Href;
                }
                return Created(uri, value);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }

        // PUT api/<ArtistsController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Artist value, LinkGenerator linkGenerator)
        {
            try
            {
                HttpContext context = Request.HttpContext;
                await museumDbRepository.UpdateArtist(value);
                return Ok(value);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }

        //DELETE api/<ArtistsController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpContext context = Request.HttpContext;
                bool deleted = await museumDbRepository.DeleteArtist(id);
                return Ok("Artista borrado");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }
    }
}
