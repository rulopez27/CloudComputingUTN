using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CloudComputingUTN.Entities;
using CloudComputingUTN.WebApp.Data;
using CloudComputingUTN.Middleware;

namespace CloudComputingUTN.WebApp.Controllers
{
    public class ArtworksController : Controller
    {
        private IMuseumDbRepository MuseumDbRepository;

        public ArtworksController(IMuseumDbRepository repository)
        {
            MuseumDbRepository = repository;
        }

        // GET: Artworks
        public async Task<IActionResult> Index()
        {
            var artworks = await MuseumDbRepository.GetArtworks();
            return artworks != null ?
                View(artworks.ToList())
                : Problem("Entity set 'MuseumDbContext.Artworks' is null");
        }

        // GET: Artworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var artwork = await MuseumDbRepository.GetArtworkById(id.Value);
            if (id == null || artwork == null)
            {
                return NotFound();
            }

            return View(artwork);
        }

        // GET: Artworks/Create
        public async Task<IActionResult> Create()
        {
            var artists = await MuseumDbRepository.GetArtists();
            ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName");
            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtworkId,ArtistId,ArtworkName,ArtworkYear,ArtworkDescription,ArtworkURL")] Artwork artwork)
        {
            var artists = await MuseumDbRepository.GetArtists();
            if (ModelState.IsValid)
            {
                await MuseumDbRepository.CreateArtwork(artwork);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName", artwork.ArtistId);
            return View(artwork);
        }

        // GET: Artworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var artists = await MuseumDbRepository.GetArtists();
            var artwork = MuseumDbRepository.GetArtworkById(id.Value);
            if (id == null || artwork == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName");
            return View(artwork);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtworkId,ArtistId,ArtworkName,ArtworkYear,ArtworkDescription,ArtworkURL")] Artwork artwork)
        {
            var artists = await MuseumDbRepository.GetArtists();
            if (id != artwork.ArtworkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var foundArtwork = await MuseumDbRepository.GetArtworkById(id);
                    if (foundArtwork == null)
                    {
                        return NotFound();
                    }
                    await MuseumDbRepository.UpdateArtwork(artwork);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName", artwork.ArtistId);
            return View(artwork);
        }
    }
}
