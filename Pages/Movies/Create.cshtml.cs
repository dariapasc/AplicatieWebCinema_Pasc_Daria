using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinema.Data;
using Cinema.Models;

namespace Cinema.Pages.Movies
{
    public class CreateModel : MovieCategoriesPageModel
    {
        private readonly Cinema.Data.CinemaContext _context;

        public CreateModel(Cinema.Data.CinemaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DirectorID"] = new SelectList(_context.Set<Director>(), "DirectorID", "DirectorName");
            var movie = new Movie();
            movie.MovieCategories = new List<MovieCategory>();
            PopulateAssignedCategoryData(_context, movie);
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newMovie = new Movie();
            if (selectedCategories != null)
            {
                newMovie.MovieCategories = new List<MovieCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new MovieCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newMovie.MovieCategories.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Movie>(
            newMovie,
            "Movie",
            i => i.MovieTitle, i => i.Genre,
            i => i.TicketPrice, i => i.ReleaseDate, i => i.DirectorID))
            {
                _context.Movie.Add(newMovie);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newMovie);
            return Page();
        }
    }
}
