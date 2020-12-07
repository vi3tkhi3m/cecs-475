using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovieAm.DataAm;
using MvcMovieAm.Models;

namespace MvcMovieAm.Controllers
{
    public class MoviesAmController : Controller
    {
        private readonly MvcMovieAmContext _context;

        public MoviesAmController(MvcMovieAmContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreAmViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: MoviesAm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieAm = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieAm == null)
            {
                return NotFound();
            }

            return View(movieAm);
        }

        // GET: MoviesAm/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoviesAm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] MovieAm movieAm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieAm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieAm);
        }

        // GET: MoviesAm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieAm = await _context.Movie.FindAsync(id);
            if (movieAm == null)
            {
                return NotFound();
            }
            return View(movieAm);
        }

        // POST: MoviesAm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] MovieAm movieAm)
        {
            if (id != movieAm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieAm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieAmExists(movieAm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieAm);
        }

        // GET: MoviesAm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieAm = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieAm == null)
            {
                return NotFound();
            }

            return View(movieAm);
        }

        // POST: MoviesAm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieAm = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movieAm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieAmExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
