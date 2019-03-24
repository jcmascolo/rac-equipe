using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RacEquipe.Entity;
using RacEquipe.Models;

namespace RacEquipe.Pages.Utilisateurs
{
    public class IndexModel : PageModel
    {
        private readonly RacEquipe.Entity.RacDataContext _context;

        public IndexModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
        }

        public IList<Utilisateur> Utilisateur { get;set; }

        public async Task OnGetAsync()
        {
            Utilisateur = await _context.Utilisateurs.ToListAsync();
        }
    }
}
