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

namespace RacEquipe.Pages.Equipements
{
    public class EditModel : PageModel
    {
        private readonly RacEquipe.Entity.RacDataContext _context;

        public EditModel(RacEquipe.Entity.RacDataContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Equipement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipementExists(Equipement.EquipementId))
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

        private bool EquipementExists(int id)
        {
            return _context.Equipement.Any(e => e.EquipementId == id);
        }
    }
}
