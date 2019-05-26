using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/movies
        public IEnumerable<Movies> GetMovies()
        {
            return _context.Movies.ToList();
        }

        //GET /api/movies/id
        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(movie);
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovies(Movies movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + movie.Id) , movie);
        }

        //POST /api/movies/id
        [HttpPut]
        public void UpdateMovies(int id)
        {
            if (ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.SaveChanges();
        }

        //POST /api/movies/id
        [HttpDelete]
        public void DeleteMovies(int id)
        {
            var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
