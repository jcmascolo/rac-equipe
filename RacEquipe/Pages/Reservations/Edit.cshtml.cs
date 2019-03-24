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
        private readonly RacEquipeServices _racEquipeServices;

        public EditModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
            _racEquipeServices = new RacEquipeServices(_context);
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            bool premierEssaie = Reservation == null ? true : false;

            Reservation = await _context.Reservations
                .Include(r => r.Equipement)
                .Include(r => r.Utilisateur)
                .FirstOrDefaultAsync(m => m.ReservationId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            ViewData["EquipementId"] = new SelectList(_context.Equipement, "EquipementId", "EquipementId");
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId");
            ViewData["ReservationCompletee"] = premierEssaie ? premierEssaie : Reservation.ReservationCompletee;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Equipement equipement = _context.Equipement.FirstOrDefault(e => e.EquipementId == Reservation.EquipementId);
            Utilisateur utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == Reservation.UtilisateurId);
            ReservationRequest reservationRequest = new ReservationRequest 
            {
                Equipement = equipement,
                Utilisateur = utilisateur,
                DateFrom = Reservation.DateFrom,
                DateTo = Reservation.DateTo,
                ReservationId = Reservation.ReservationId
            };
            Reservation.ReservationCompletee = await _racEquipeServices.Reserver(reservationRequest);
            
            if (!ModelState.IsValid || !Reservation.ReservationCompletee)
            {
                return await OnGetAsync(Reservation.ReservationId);
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
