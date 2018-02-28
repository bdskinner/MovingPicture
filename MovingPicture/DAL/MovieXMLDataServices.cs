using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using MovingPicture.Models;

namespace MovingPicture.DAL
{
    public class MovieXMLDataService : IMovieDataService, IDisposable
    {
        #region METHODS

        public void Dispose()
        {

        }

        public List<Movie> Read()
        {
            //Variable Declarations.
            Movies moviesObject;

            //Initialize a filestream object to read the movie information from the xml file.
            string xmlFilePath = HttpContext.Current.Application["dataFilePath"].ToString();
            StreamReader sr = new StreamReader(xmlFilePath);

            //Initalize the xml serializer object.
            XmlSerializer deserializer = new XmlSerializer(typeof(Movies));

            using (sr)
            {
                //Read the data from the xml file into a generic object.
                object xmlObject = deserializer.Deserialize(sr);

                //Cast the generic object to the Movie class.
                moviesObject = (Movies)xmlObject;
            }

            //Return the list of movies.
            return moviesObject.movies;
        }

        public void Write(List<Movie> movies)
        {
            //Initialize a filestream object to read the movie information from the xml file.
            string xmlFilePath = HttpContext.Current.Application["dataFilePath"].ToString();
            StreamWriter sw = new StreamWriter(xmlFilePath, false);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Movie>), new XmlRootAttribute("Movies"));

            using (sw)
            {
                serializer.Serialize(sw, movies);
            }
        }

        #endregion
    }
}