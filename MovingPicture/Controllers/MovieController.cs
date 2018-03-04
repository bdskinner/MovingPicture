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
        [HttpGet]
        public ActionResult Create()
        {
            //Get a list of genres for the "Filter by Genre" list.
            ViewBag.Genres = ListOfGenres();

            //Display the view.
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            try
            {
                //Create a new instance of the movie repository.
                MovieRepository movieRepository = new MovieRepository();

                using (movieRepository)
                {
                    //Get the ID value for the genre selected.
                    movie.GenreID = GetGenreID(movie.GenreTitle);

                    //Add the new movie.
                    movieRepository.Insert(movie);
                }

                //Return the user to the list of movies.
                return RedirectToAction("Index");
            }
            catch
            {
                //If an error occurred display the error page.
                return View("Error");
            }
        }      
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                //Variable Declarations.
                Movie movie = new Movie();
                MovieRepository movieRepository = new MovieRepository();

                //Get the current information for the movie with the ID value provided.
                using (movieRepository)
                {
                    movie = movieRepository.SelectOne(id);
                }

                //Return the view with the information.
                return View(movie);
            }
            catch
            {
                //If an error occured display an error message.
                return View("Error");
            }
        }
        
        [HttpPost]
        public ActionResult Delete(int id, Movie movie)
        {
            try
            {
                //Variable Declarations.
                MovieRepository movieRepository = new MovieRepository();

                //Get the current information for the movie with the ID value provided.
                using (movieRepository)
                {
                    movieRepository.Delete(id);
                }

                //Return to the list of movies.
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
        
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                //Variable Declarations.
                Movie movie = new Movie();
                MovieRepository movieRepository = new MovieRepository();

                //Get a list of genres for the "Filter by Genre" list.
                ViewBag.Genres = ListOfGenres();

                //Get the current information for the movie with the ID value provided.
                using (movieRepository)
                {
                    movie = movieRepository.SelectOne(id);
                }

                //Return the view with the information.
                return View(movie);
            }
            catch
            {
                //If an error occured display an error message.
                return View("Error");
            }
        }
        
        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            try
            {
                //Variable Declarations.
                MovieRepository movieRepository = new MovieRepository();

                //Save the updated information for the selected movie.
                using (movieRepository)
                {
                    //Get the ID value for the genre selected.
                    movie.GenreID = GetGenreID(movie.GenreTitle);

                    //Update the movie's information with the information entered by the user.
                    movieRepository.Update(movie);
                }

                //Return to the list of movies.
                return RedirectToAction("Index");
            }
            catch
            {
                //If an error occured display an error message.
                return View("Error");
            }
        }

        private int GetGenreID(string genreTitle)
        {
            //Variable Declarations.
            int genreID = 0;
            MovieRepository movieRepository = new MovieRepository();
            IEnumerable<Movie> movies;

            //Get the list of movies.
            using (movieRepository)
            {
                movies = movieRepository.SelectAll() as IList<Movie>;
            }

            //Get the ID value for the genre based on the description.
            genreID = (from m in movies
                       where m.GenreTitle == genreTitle
                       select m.GenreID).FirstOrDefault();


            //Return the ID value for the genre.
            return genreID;
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
            int pageSize = 50;
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
            int pageSize = 50;
            int pageNumber = (page ?? 1);

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
                movies = from m in movies
                        where m.Title.ToUpper().Contains(searchCriteria.ToUpper())
                        select m;
            }

            if (genreFilter != "" || genreFilter == null)
            {
                movies = from m in movies
                         where m.GenreTitle == genreFilter
                         select m;
            }

            //Set Paginate settings.
            movies = movies.ToPagedList(pageNumber, pageSize);

            //Return the view.
            return View(movies);
        }
    }
}
