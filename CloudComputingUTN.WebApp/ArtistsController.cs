using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;

namespace CloudComputingUTN.WebApp
{
    public class ArtistsController : Controller
    {
        private IMuseumDbRepository MuseumDbRepository;

        public ArtistsController(IMuseumDbRepository repository)
        {
            this.MuseumDbRepository = repository;
        }


        // GET: Artists
        public async Task<IActionResult> Index()
        {
            var artists = await MuseumDbRepository.GetArtists();
              return artists != null ? 
                          View(artists.ToList()) :
                          Problem("Entity set 'MuseumDbContext.Artists'  is null.");
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await MuseumDbRepository.GetArtists() == null)
            {
                return NotFound();
            }

            var artist = await MuseumDbRepository.GetArtistById(id.Value);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtistId,ArtistName,ArtistWikiPage")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                await MuseumDbRepository.CreateArtist(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await MuseumDbRepository.GetArtists() == null)
            {
                return NotFound();
            }

            var artist = await MuseumDbRepository.GetArtistById(id.Value);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtistId,ArtistName,ArtistWikiPage")] Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var foundArtist = await MuseumDbRepository.GetArtistById(id);
                    if (foundArtist == null)
                    {
                        return NotFound();
                    }
                    await MuseumDbRepository.UpdateArtist(artist);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }
    }
}
