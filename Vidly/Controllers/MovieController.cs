using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Random
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(x => x.Genre).ToList();

            MovieViewModel viewModel = new MovieViewModel()
            {
                Movies = movies
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(x => x.Genre).FirstOrDefault(y => y.Id == id);

            if (movies == null)
                return HttpNotFound();

            return View(movies);
        }

        public ActionResult Edit(int id)
        {
            Movies movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            MovieFormViewModel viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult New()
        {
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movies movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList(),
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                Movies movieInDb = _context.Movies.Include(m => m.Genre).FirstOrDefault(x => x.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.DateAdded = DateTime.Now;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movie");
        }
    }
}