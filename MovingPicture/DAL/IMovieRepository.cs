using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovingPicture.Models;

namespace MovingPicture.DAL
{
    public interface IMovieRepository
    {
    #region METHODS

        IEnumerable<Movie> SelectAll();

        Movie SelectOne(int ID);

        void Insert(Movie movie);

        void Update(Movie movie);

        void Delete(int ID);

    #endregion  
    }
}
