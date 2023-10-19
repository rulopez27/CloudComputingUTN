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
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var artist = await museumDbRepository.GetArtistById(id);
                var artistDto = _mapper.Map<ArtistDto>(artist);
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
        public void Post([FromBody] Artist value)
        {
        }

        // PUT api/<ArtistsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Artist value)
        {
        }
    }
}
