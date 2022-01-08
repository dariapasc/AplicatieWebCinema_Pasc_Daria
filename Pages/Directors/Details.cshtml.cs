using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;

namespace Cinema.Pages.Directors
{
    public class DetailsModel : PageModel
    {
        private readonly Cinema.Data.CinemaContext _context;

        public DetailsModel(Cinema.Data.CinemaContext context)
        {
            _context = context;
        }

        public Director Director { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Director = await _context.Director.FirstOrDefaultAsync(m => m.DirectorID == id);

            if (Director == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
