using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RacEquipe.Entity;
using RacEquipe.Models;

namespace RacEquipe.Pages.Utilisateurs
{
    public class EditModel : PageModel
    {
        private readonly RacEquipe.Entity.RacDataContext _context;

        public EditModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(m => m.UtilisateurId == id);

            if (Utilisateur == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(Utilisateur.UtilisateurId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
        }
    }
}
