using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;

namespace Cinema.Pages.Movies
{
    public class EditModel : MovieCategoriesPageModel
    {
        private readonly Cinema.Data.CinemaContext _context;

        public EditModel(Cinema.Data.CinemaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie
                .Include(b => b.Director)
                .Include(b => b.MovieCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MovieID == id);

            if (Movie == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Movie);
            ViewData["DirectorID"] = new SelectList(_context.Set<Director>(), "DirectorID", "DirectorName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movieToUpdate = await _context.Movie
            .Include(i => i.Director)
            .Include(i => i.MovieCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.MovieID == id);

            if (movieToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Movie>(
            movieToUpdate,
            "Movie",
            i => i.MovieTitle, i => i.Genre,
            i => i.TicketPrice, i => i.ReleaseDate, i => i.Director))
            {
                UpdateMovieCategories(_context, selectedCategories, movieToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Movies care este editata
            UpdateMovieCategories(_context, selectedCategories, movieToUpdate);
            PopulateAssignedCategoryData(_context, movieToUpdate);
            return Page();
        }
    }
}


