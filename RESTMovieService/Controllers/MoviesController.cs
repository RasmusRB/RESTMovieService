using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLib.Model;
using RESTMovieService.DBUtil;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTMovieService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        ManageMovies mm = new ManageMovies();

        private static readonly List<Movies> movies = new List<Movies>()
        {
            new Movies(1, "The Fellowship of the Ring", "Peter Jackson", "Fantasy", 2001, 8.8),
            new Movies(2, "The Two Towers", "Peter Jackson", "Fantasy", 2002, 8.7),
            new Movies(3, "Return of the King", "Peter Jackson", "Fantasy", 2003, 8.9),
            new Movies(4, "The Terminator", "James Cameron", "Science Fiction", 1984, 8.0),
            new Movies(5, "Terminator 2: Judgment Day", "James Cameron", "Science Fiction", 1991, 8.5),
            new Movies(6, "Terminator 3: Rise of the Machines", "Jonathan Mostow", "Science Fiction", 2003, 6.3)
        };

        // GET from substring - fx. api/movies/Title/The Terminator 
        [HttpGet]
        [Route("Title/{substring}")]
        public IEnumerable<Movies> GetFromSubstring(String substring)
        {
            return movies.FindAll(m => m.Title.Contains(substring));
        }

        // GET from substring - fx. api/movies/Genre/Science Fiction - watch capital letters
        [HttpGet]
        [Route("Genre/{substring}")]
        public IEnumerable<Movies> GetByGenre (String substring)
        {
            return movies.FindAll(m => m.Genre.Contains(substring));
        }

        // GET: api/<MoviesController>
        [HttpGet]
        public IEnumerable<Movies> Get()
        {
            return mm.Get();
        }

        // GET api/<MoviesController>/5
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByIdStatusCodes(int id)
        {
            if (movies.Exists(m => m.Id == id))
            {
                return Ok(movies.Find(m => m.Id == id));
            }

            return NotFound($"Movie id : {id} not found");
        }

        [HttpGet]
        [Route("{id}")]
        public Movies GetById(int id)
        {
            return mm.GetById(id);
        }

        // POST api/<MoviesController>
        [HttpPost]
        public void Post([FromBody] Movies value)
        {
           mm.Post(value);
        }

        // PUT api/<MoviesController>/5
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] Movies value)
        {
            mm.Put(id, value);
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            mm.Delete(id);
        }
    }
}
