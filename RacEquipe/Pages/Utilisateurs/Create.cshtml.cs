using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RacEquipe.Entity;
using RacEquipe.Models;

namespace RacEquipe.Pages.Utilisateurs
{
    public class CreateModel : PageModel
    {
        private readonly RacEquipe.Entity.RacDataContext _context;

        public CreateModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Utilisateurs.Add(Utilisateur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}