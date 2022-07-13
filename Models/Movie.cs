using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace L10.Models
{
    public class Movie
    {
        //MovieID
        public int Mid { get; set; }

        [Required(ErrorMessage = "Please enter Title")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title 1-50 chars")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter name of Director ")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Director 1-40 chars")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Please enter Date/Time")]
        [DataType(DataType.DateTime)]
        [Remote(action: "VerifyDate", controller: "Movie")]
        public DateTime PerformDT { get; set; }

        [Required(ErrorMessage = "Please enter Duration")]
        [Range(0.5, 4.0, ErrorMessage = "Duration 0.5-4.0 hours")]
        public float Duration { get; set; }

        [Required(ErrorMessage = "Please enter Price")]
        [Range(0.0, 1000.0, ErrorMessage = "Price 0-1000")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Please enter Theater")]
        [RegularExpression("T[1-3][0-9]", ErrorMessage = "Invalid Character")]
        public string Theater { get; set; }

    }
}