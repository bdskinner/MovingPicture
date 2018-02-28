using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovingPicture.DAL;
using MovingPicture.Models;
using PagedList;

namespace MovingPicture.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }      

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Details/5
        public ActionResult Details(int ID)
        {
            //Variable Declarations.
            MovieRepository movieRepository = new MovieRepository();
            Movie movie = new Movie();

            using (movieRepository)
            {
                movie = movieRepository.SelectOne(ID);
            }

            //Return the view;
            return View(movie);
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<string> ListOfGenres()
        {
            //Variable Declarations.
            MovieRepository movieReporsitory = new MovieRepository();
            IEnumerable<Movie> movies;
            IEnumerable<string> movieGenres = null;

            //Get a list of all movies.
            using (movieReporsitory)
            {
                movies = movieReporsitory.SelectAll() as IList<Movie>;
            }

            //Get a list of genres.
            movieGenres = (from m in movies
                           orderby m.GenreTitle ascending
                           select m.GenreTitle).Distinct() as IEnumerable<string>;

            //Return the list of genres.
            return movieGenres as IEnumerable<string>;
        }

        [HttpGet]
        public ActionResult Index(string sortOrder, int? page)
        {
            //Variable Declarations.
            MovieRepository movieRepository = new MovieRepository();
            IEnumerable<Movie> movies;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //Get the list of movies.
            using (movieRepository)
            {
                movies = movieRepository.SelectAll() as IList<Movie>;
            }

            //Get a list of genres for the "Filter by Genre" list.
            ViewBag.Genres = ListOfGenres();

            //Sort the records.
            switch (sortOrder)
            {
                case "Title":
                    movies = from m in movies
                             orderby m.Title ascending
                             select m;
                    break;
                case "Length":
                    movies = from m in movies
                             orderby m.MovieLength ascending
                             select m;
                    break;
                case "Genre":
                    movies = from m in movies
                             orderby m.GenreTitle ascending
                             select m;
                    break;
                default:
                    //If no sort order is specified sort by the movie's title.
                    movies = from m in movies
                             orderby m.Title ascending
                             select m;
                    break;
            }

            //Set Paginate settings.
            movies = movies.ToPagedList(pageNumber, pageSize);

            //Return the view.
            return View(movies);
        }

        [HttpPost]
        public ActionResult Index(string searchCriteria, string genreFilter, int? page)
        {
            //Variable Declarations.
            MovieRepository movieReporsitory = new MovieRepository();
            IEnumerable<Movie> movies;
            IEnumerable<Movie> movieResults = null;

            //Get a list of all movies.
            using (movieReporsitory)
            {
                movies = movieReporsitory.SelectAll() as IList<Movie>;
            }

            //Get a list of genres for the "Filter by Genre" list.
            ViewBag.Genres = ListOfGenres();

            //If search criteria was passed to the action search by the title entered.
            if (searchCriteria != null)
            {
                movieResults = from m in movies
                               where m.Title.ToUpper().Contains(searchCriteria.ToUpper())
                               select m;
            }

            if (genreFilter != "" || genreFilter == null)
            {
                movieResults = from m in movieResults
                               where m.GenreTitle == genreFilter
                               select m;
            }

            //Return the view.
            return View(movieResults);
        }
    }
}
