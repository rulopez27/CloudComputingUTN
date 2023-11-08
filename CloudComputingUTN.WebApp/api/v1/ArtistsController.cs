using AutoMapper;
using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.WebApp.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudComputingUTN.WebApp.api.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private IMuseumDbRepository museumDbRepository;
        private IMapper _mapper;
        public ArtistsController(IMuseumDbRepository repository, IMapper mapper)
        {
            museumDbRepository = repository;
            _mapper = mapper;
        }

        // GET api/<ArtistsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, LinkGenerator linkGenerator)
        {
            try
            {
                HttpContext context = Request.HttpContext;
                if (id == 0)
                {
                    return BadRequest();
                }
                var artist = await museumDbRepository.GetArtistById(id);
                var artistDto = _mapper.Map<ArtistDto>(artist);
                
                artistDto.Links.Add(new Link(linkGenerator.GetUriByAction(context, "Get"), "self", "GET"));
                artistDto.Links.Add(new Link(linkGenerator.GetUriByAction(context, "Post"), "create", "POST"));
                artistDto.Links.Add(new Link(linkGenerator.GetUriByAction(context, "Put", "Artists", new { id }), "update", "PUT"));

                return Ok(artistDto);
            }
            catch(InvalidOperationException ioex)
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
                HttpContext context = Request.HttpContext;

                await museumDbRepository.CreateArtist(value);
                string uri = linkGenerator.GetUriByAction(context, "Get", "Artists", new {id = value.ArtistId});

                return Created(uri, value);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return StatusCode(500, errorMessage);
            }
        }

        // PUT api/<ArtistsController>/5
        [HttpPut("{id}")]
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
    }
}
