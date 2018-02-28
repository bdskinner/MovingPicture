using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovingPicture.Models
{
    public class Movie
    {

        #region PROPERTIES

        public int MovieID { get; set; }

        public string Title { get; set; }

        public string Synopsis { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int GenreID { get; set; }

        [Display(Name = "Genre")]
        public string GenreTitle { get; set; }

        public string Director { get; set; }

        [Display(Name = "Length")]
        public int MovieLength { get; set; }

        #endregion

        #region CONSTRUCTOR(S)



        #endregion

        #region METHODS



        #endregion  
    }
}