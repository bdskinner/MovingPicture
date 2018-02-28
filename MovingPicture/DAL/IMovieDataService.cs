using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovingPicture.Models;

namespace MovingPicture.DAL
{
    public interface IMovieDataService
    {
        List<Movie> Read();

        void Write(List<Movie> movies);
    }
}
