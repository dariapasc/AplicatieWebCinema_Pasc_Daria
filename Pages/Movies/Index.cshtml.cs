using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;

namespace Cinema.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Cinema.Data.CinemaContext _context;

        public IndexModel(Cinema.Data.CinemaContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }
        public MovieData MovieD { get; set; }
        public int MovieID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            MovieD = new MovieData();

            MovieD.Movies = await _context.Movie
            .Include(b => b.Director)
            .Include(b => b.MovieCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.MovieTitle)
            .ToListAsync();
            if (id != null)
            {
                MovieID = id.Value;
                Movie movie = MovieD.Movies
                .Where(i => i.MovieID == id.Value).Single();
                MovieD.Categories = movie.MovieCategories.Select(s => s.Category);
            }
        }
    }
    }
