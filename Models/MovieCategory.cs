using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class MovieCategory
    {
        public int MovieCategoryID { get; set; }
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
