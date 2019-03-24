using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RacEquipe.Entity;
using RacEquipe.Models;

namespace RacEquipe.Pages.Reservations
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
        ViewData["EquipementId"] = new SelectList(_context.Equipement, "EquipementId", "EquipementId");
        ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId");
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            IEnumerable<Reservation> reservations = _context.Reservations;
            bool reservationPourMemePlageHoraire = Reservation.ReservationPourMemePlageHoraireExiste(reservations);
            
            if (!ModelState.IsValid || reservationPourMemePlageHoraire)
            {
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}