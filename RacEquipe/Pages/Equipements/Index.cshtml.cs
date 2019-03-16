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
    public class IndexModel : PageModel
    {
        private readonly RacEquipe.Entity.RacDataContext _context;

        public IndexModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
        }

        public IList<Equipement> Equipement { get;set; }

        public async Task OnGetAsync()
        {
            Equipement = await _context.Equipement.ToListAsync();
        }
    }
}
