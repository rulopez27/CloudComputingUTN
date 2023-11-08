﻿using AutoMapper;
using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.WebApp.DataAccessLayer;
using CloudComputingUTN.WebApp.Extensions;
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
        private IHttpContextAccessor _contextAccessor;
        public ArtistsController(IMuseumDbRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            museumDbRepository = repository;
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
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
                artistDto.CreateArtistLinks(linkGenerator, _contextAccessor);

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
    }
}
