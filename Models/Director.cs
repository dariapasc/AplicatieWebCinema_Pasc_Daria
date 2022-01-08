using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Director
    {
        public int DirectorID { get; set; }
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }
        public ICollection<Movie> Movies { get; set; } //navigation property
    }

}
