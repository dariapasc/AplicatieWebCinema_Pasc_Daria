using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinema.Data;
using Cinema.Models;

namespace Cinema.Pages.Directors
{
    public class CreateModel : PageModel
    {
        private readonly Cinema.Data.CinemaContext _context;

        public CreateModel(Cinema.Data.CinemaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Director Director { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Director.Add(Director);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
