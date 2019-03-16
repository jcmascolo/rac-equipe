using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RacEquipe.Entity;
using RacEquipe.Models;

namespace RacEquipe.Pages.Equipements
{
    public class DeleteModel : PageModel
    {
        private readonly RacEquipe.Entity.RacDataContext _context;

        public DeleteModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Equipement Equipement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipement = await _context.Equipement.FirstOrDefaultAsync(m => m.EquipementId == id);

            if (Equipement == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipement = await _context.Equipement.FindAsync(id);

            if (Equipement != null)
            {
                _context.Equipement.Remove(Equipement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
