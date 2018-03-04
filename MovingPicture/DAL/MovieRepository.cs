using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovingPicture.Models;

namespace MovingPicture.DAL
{
    public class MovieRepository : IMovieRepository, IDisposable
    {
        #region FIELDS

        private List<Movie> _movies;

        #endregion

        #region CONSTRUCTOR(S)

        public MovieRepository()
        {
            MovieXMLDataService moviexmldataservice = new MovieXMLDataService();

            using (moviexmldataservice)
            {
                _movies = moviexmldataservice.Read() as List<Movie>;
            }
        }

        #endregion

        #region METHODS

        public void Delete(int ID)
        {
            //Get the movie information for the movie to be delted.
            var oldMovie = (from m in _movies
                            where m.MovieID == ID
                            select m).SingleOrDefault();

            //If the movie information is found remove the movie's information from the list.
            if (oldMovie != null)
            {
                _movies.Remove(oldMovie);
            }

            //Save the updated movie list to the XML file.
            Save();
        }

        public void Dispose()
        {
            _movies = null;
        }

        public void Insert(Movie movie)
        {
            movie.MovieID = NextIDValue();
            _movies.Add(movie);

            Save();
        }

        private int NextIDValue()
        {
            //Get the largest ID value for the movies currently stored in the XML file.
            int currentMaxID = (from m in _movies
                                orderby m.MovieID descending
                                select m).FirstOrDefault().MovieID;

            //Return the next number in the sequence after the max ID value.
            return currentMaxID + 1;
        }

        public void Save()
        {
            //Variable Declarations.
            MovieXMLDataService movieXMLDataService = new MovieXMLDataService();

            //Save the list of movies to the XML File.
            using (movieXMLDataService)
            {
                movieXMLDataService.Write(_movies);
            }
        }

        public IEnumerable<Movie> SelectAll()
        {
            return _movies;
        }

        public Movie SelectOne(int ID)
        {                  
            //Ge the movie information for the movie with the ID value provided.
            var selectedMovie = (from m in _movies
                                where m.MovieID == ID
                                select m).FirstOrDefault();

            //Return the movie information for the selected movie.
            return selectedMovie as Movie;
        }

        public void Update(Movie movie)
        {
            //Get the original movie information for the movie to be updated.
            var oldMovie = (from m in _movies
                            where m.MovieID == movie.MovieID
                            select m).SingleOrDefault();

            //If the movie information is found update the movie's information.
            if (oldMovie != null)
            {
                _movies.Remove(oldMovie);
                _movies.Add(movie);
            }

            //Save the update movie information to the XML file.
            Save();
        }

        #endregion
    }
}