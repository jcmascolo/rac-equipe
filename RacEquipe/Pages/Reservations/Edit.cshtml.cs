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

namespace RacEquipe.Pages.Reservations
{
    public class EditModel : PageModel
    {
        private readonly RacEquipe.Entity.RacDataContext _context;

        public EditModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation = await _context.Reservations
                .Include(r => r.Equipement)
                .Include(r => r.Utilisateur).FirstOrDefaultAsync(m => m.ReservationId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
           ViewData["EquipementId"] = new SelectList(_context.Equipement, "EquipementId", "EquipementId");
           ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IEnumerable<Reservation> reservations = _context.Reservations;
            bool reservationPourMemePlageHoraire = Reservation.ReservationPourMemePlageHoraireExiste(reservations);
            
            if (!ModelState.IsValid || reservationPourMemePlageHoraire)
            {
                return Page();
            }

            _context.Attach(Reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.ReservationId))
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

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}
