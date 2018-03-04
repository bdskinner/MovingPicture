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

        [Required]
        public string Title { get; set; }

        [Required]
        public string Synopsis { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date (MM/DD/YYYY)")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public int GenreID { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public string GenreTitle { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        [Range(60, 500, ErrorMessage = "Please enter valid integer between 60 and 500.")]        
        [Display(Name = "Length")]
        public int MovieLength { get; set; }

        #endregion

        #region CONSTRUCTOR(S)



        #endregion

        #region METHODS



        #endregion  
    }
}