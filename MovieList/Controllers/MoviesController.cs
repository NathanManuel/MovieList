using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieList.Data;
using MovieList.Models;

namespace MovieList.Controllers
{
    public class MoviesController : Controller
    {

        static List<Movie> movies = new List<Movie>()
        {
            new Movie() { ID = Guid.NewGuid(), MoveiL = 140, MovieName= "Thor" },
            new Movie() { ID = Guid.NewGuid(), MoveiL = 120, MovieName= "Captin America" },
            new Movie() { ID = Guid.NewGuid(), MoveiL = 200, MovieName= "Star Wars" }
        };

        private readonly MovieListContext _context;

        public MoviesController(MovieListContext context)
        {
            _context = context;
        }

        // GET: Movies
        public IActionResult Index()
        {
            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = movies.FirstOrDefault(x => x.ID == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Movie movie) 
        {
            if (ModelState.IsValid)
            {
                movie.ID = Guid.NewGuid();

                movies.Add(movie);

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = movies.FirstOrDefault(x => x.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,MovieName,MoveiL")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentMovie = movies.FirstOrDefault(x => x.ID == id);
                currentMovie.MovieName = movie.MovieName;
                currentMovie.MoveiL = movie.MoveiL;

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = movies.FirstOrDefault(x => x.ID == id);
          
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);

        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var movie = movies.FirstOrDefault(x => x.ID == id);
            movies.Remove(movie);
            return RedirectToAction(nameof(Index));

        }

        private bool MovieExists(Guid id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
