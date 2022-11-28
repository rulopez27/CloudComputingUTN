using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.WebApp.Models;

namespace CloudComputingUTN.WebApp.Controllers
{
    public class ArtistsController : Controller
    {
        private IMuseumDbRepository MuseumDbRepository;

        public ArtistsController(IMuseumDbRepository repository)
        {
            MuseumDbRepository = repository;
        }


        // GET: Artists
        public async Task<IActionResult> Index()
        {
            ArtistsListViewModel artistsListViewModel = new ArtistsListViewModel();
            try
            {
                var artists = await MuseumDbRepository.GetArtists();
                artistsListViewModel.Artists = artists.ToList();
            }
            catch (Exception ex)
            {
                artistsListViewModel.ClassName = "alert alert-danger";
                artistsListViewModel.Title = "Error";
                artistsListViewModel.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            }
            return View(artistsListViewModel);
            
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ArtistViewModel artistViewModel = new ArtistViewModel();
            try
            {
                if (id == null)
                {
                    artistViewModel.Title = "Error";
                    artistViewModel.Message = "Invalid ID";
                    artistViewModel.ClassName = "alert alert-danger";
                    artistViewModel.RecordFound = false;
                    return View(artistViewModel);
                }
                var artist = await MuseumDbRepository.GetArtistById(id.Value);
                if (artist == null)
                {
                    artistViewModel.Title = "Error";
                    artistViewModel.Message = "Record not found";
                    artistViewModel.ClassName = "alert alert-danger";
                    artistViewModel.RecordFound = false;
                    return View(artistViewModel);
                }
                artistViewModel.Artist = artist;
            }
            catch (Exception ex)
            {
                artistViewModel.ClassName = "alert alert-danger";
                artistViewModel.Title = "Error";
                artistViewModel.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                artistViewModel.RecordFound = false;
            }
            return View(artistViewModel);

        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View(new ArtistViewModel());
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Artist, ClassName, Message, Title")] ArtistViewModel artistViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await MuseumDbRepository.CreateArtist(artistViewModel.Artist);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    artistViewModel.ClassName = "alert alert-danger";
                    artistViewModel.Title = "Error";
                    artistViewModel.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    return View(artistViewModel);
                }
                
            }
            return View(artistViewModel);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ArtistViewModel artistViewModel = new ArtistViewModel();
            try
            {
                if (id == null)
                {
                    artistViewModel.Title = "Error";
                    artistViewModel.Message = "Invalid ID";
                    artistViewModel.ClassName = "alert alert-danger";
                    artistViewModel.RecordFound = false;
                    return View(artistViewModel);
                }
                var artist = await MuseumDbRepository.GetArtistById(id.Value);
                if (artist == null)
                {
                    artistViewModel.Title = "Error";
                    artistViewModel.Message = "Record not found";
                    artistViewModel.ClassName = "alert alert-danger";
                    artistViewModel.RecordFound = false;
                    return View(artistViewModel);
                }
                artistViewModel.Artist = artist;
            }
            catch (Exception ex)
            {
                artistViewModel.ClassName = "alert alert-danger";
                artistViewModel.Title = "Error";
                artistViewModel.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                artistViewModel.RecordFound = false;
            }
            return View(artistViewModel);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Artist, ClassName, Message, Title")] ArtistViewModel artistViewModel)
        {
            if (id != artistViewModel.Artist.ArtistId)
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
                    await MuseumDbRepository.UpdateArtist(artistViewModel.Artist);
                }
                catch(Exception ex)
                {
                    artistViewModel.ClassName = "alert alert-danger";
                    artistViewModel.Title = "Error";
                    artistViewModel.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    return View(artistViewModel);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artistViewModel);
        }
    }
}
