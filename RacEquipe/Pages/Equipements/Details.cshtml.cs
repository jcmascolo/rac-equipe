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
    public class DetailsModel : PageModel
    {
        private readonly RacEquipe.Entity.RacDataContext _context;

        public DetailsModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
        }

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
    }
}
