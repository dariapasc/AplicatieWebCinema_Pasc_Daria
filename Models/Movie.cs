using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Movie
    {
        public int MovieID { get; set; }

        [Required, StringLength(150, MinimumLength = 3)]
        [Display(Name = "Movie Title")]
        public string MovieTitle { get; set; }

        public string Genre { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 300)]
        [Display(Name = "Ticket Price")]
        [Column(TypeName = "decimal(6, 2)")] //permite valori cu doua zecimale
        public decimal TicketPrice { get; set; }

       
        public int DirectorID { get; set; }
        public Director Director { get; set; } //navigation property

        [Display(Name = "Categories")]
        public ICollection<MovieCategory> MovieCategories { get; set; }

    }
}
