using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            ViewData["MoviesCount"] = movieGenreVM.Movies.Count;

            return View(movieGenreVM);
        }

        //  POST: /Movies?searchString=string
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return $"Do Index, com [HttpPost]: Filtre por {searchString}";
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                ViewData["ErrorCode"] = 400;
                ViewData["ErrorMessage"] = "ID inválido!";
                return View("~/Views/Shared/Error.cshtml",
                            new Models.ErrorViewModel()
                );
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                ViewData["ErrorCode"] = 404;
                ViewData["ErrorMessage"] = "Filme não encontrado!";
                return View("~/Views/Shared/Error.cshtml",
                            new Models.ErrorViewModel()
                );
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                ViewData["ErrorCode"] = 400;
                ViewData["ErrorMessage"] = "ID inválido!";
                return View("~/Views/Shared/Error.cshtml",
                            new Models.ErrorViewModel()
                );
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                ViewData["ErrorCode"] = 404;
                ViewData["ErrorMessage"] = "Filme não encontrado!";
                return View("~/Views/Shared/Error.cshtml",
                            new Models.ErrorViewModel()
                );
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                ViewData["ErrorCode"] = 400;
                ViewData["ErrorMessage"] = "ID de edição não condiz com ID do filme.";
                return View("~/Views/Shared/Error.cshtml",
                            new Models.ErrorViewModel()
                );
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        ViewData["ErrorCode"] = 404;
                        ViewData["ErrorMessage"] = "Filme não encontrado!";
                        return View("~/Views/Shared/Error.cshtml",
                                    new Models.ErrorViewModel()
                        );
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                ViewData["ErrorCode"] = 400;
                ViewData["ErrorMessage"] = "ID inválido.";
                return View("~/Views/Shared/Error.cshtml",
                            new Models.ErrorViewModel()
                );
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                ViewData["ErrorCode"] = 404;
                ViewData["ErrorMessage"] = "Filme não encontrado!";
                return View("~/Views/Shared/Error.cshtml",
                            new Models.ErrorViewModel()
                );
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }

        // GET: Movies/DeleteAll
        public async Task<IActionResult> DeleteAll()
        {
            var movies = await (from m in _context.Movie
                                select m)
                              .ToListAsync();

            ViewData["MoviesCount"] = movies.Count;

            return View();
        }

        // POST: Movies/DeleteAll
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll(bool notUsed)
        {
            var movies = from m in _context.Movie
                         select m;

            _context.RemoveRange(movies);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
