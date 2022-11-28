using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.WebApp.Models;
using CloudComputingUTN.Entities;

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
            ArtworksListViewModel model = new ArtworksListViewModel();
            try
            {
                var artworks = await MuseumDbRepository.GetArtworks();
                model.Artworks = artworks.ToList();
            }
            catch (Exception ex)
            {
                model.ClassName = "alert alert-danger";
                model.Title = "Error";
                model.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            }

            return View(model);
        }

        // GET: Artworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ArtworkViewModel model = new ArtworkViewModel();
            try
            {
                if (!id.HasValue)
                {
                    model.ClassName = "alert alert-danger";
                    model.Title = "Error";
                    model.Message = "Invalid argument";
                    model.RecordFound = false;
                    return View(model);
                }
                var artwork = await MuseumDbRepository.GetArtworkById(id.Value);
                if (artwork == null)
                {
                    model.ClassName = "alert alert-danger";
                    model.Title = "Error";
                    model.Message = "Entry not found";
                    model.RecordFound = false;
                }
                else
                {
                    model.Artwork = artwork;
                }
            }
            catch (Exception ex)
            {
                model.ClassName = "alert alert-danger";
                model.Title = "Error";
                model.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                model.RecordFound = false;
            }
            return View(model);
        }

        // GET: Artworks/Create
        public async Task<IActionResult> Create()
        {
            ArtworkViewModel model = new ArtworkViewModel();
            try
            {
                var artists = await MuseumDbRepository.GetArtists();
                ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName");
            }
            catch (Exception ex)
            {
                model.ClassName = "alert alert-danger";
                model.Title = "Error";
                model.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            }
            return View(model);
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Artwork, Title, Message, ClassName")] ArtworkViewModel model)
        {
            ArtworkViewModel artworkViewModel = new ArtworkViewModel(model.Artwork);
            var artists = await MuseumDbRepository.GetArtists();
            if (ModelState.IsValid)
            {
                try
                {
                    await MuseumDbRepository.CreateArtwork(model.Artwork);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    artworkViewModel.ClassName = "alert alert-danger";
                    artworkViewModel.Title = "Error";
                    artworkViewModel.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName", model.Artwork.ArtistId);
                }
                
            }
            return View(artworkViewModel);
        }

        // GET: Artworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ArtworkViewModel model = new ArtworkViewModel();
            try
            {
                if (!id.HasValue)
                {
                    model.ClassName = "alert alert-danger";
                    model.Title = "Error";
                    model.Message = "Invalid argument";
                    model.RecordFound = false;
                    return View(model);
                }
                var artwork = await MuseumDbRepository.GetArtworkById(id.Value);
                if (artwork == null)
                {
                    model.ClassName = "alert alert-danger";
                    model.Title = "Error";
                    model.Message = "Entry not found";
                    model.RecordFound = false;
                }
                else
                {
                    var artists = await MuseumDbRepository.GetArtists();
                    model.Artwork = artwork;
                    ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName");
                }
            }
            catch (Exception ex)
            {
                model.ClassName = "alert alert-danger";
                model.Title = "Error";
                model.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                model.RecordFound = false;
            }
            return View(model);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Artwork, Title, Message, ClassName")] ArtworkViewModel viewModel)
        {
            var artists = await MuseumDbRepository.GetArtists();
            ArtworkViewModel artworkViewModel = new ArtworkViewModel(viewModel.Artwork);
            if (id != viewModel.Artwork.ArtworkId)
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
                    await MuseumDbRepository.UpdateArtwork(viewModel.Artwork);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    artworkViewModel.ClassName = "alert alert-danger";
                    artworkViewModel.Title = "Error";
                    artworkViewModel.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName", viewModel.Artwork.ArtistId);
                }
                
            }
            ViewData["ArtistId"] = new SelectList(artists.ToList(), "ArtistId", "ArtistName", artworkViewModel.Artwork.ArtistId);
            return View(artworkViewModel);
        }
    }
}
