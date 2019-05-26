using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movies
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Genres Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1,20)]
        [Display(Name = "Number In Stock")]
        public int NumberInStock { get; set; }
    }
}