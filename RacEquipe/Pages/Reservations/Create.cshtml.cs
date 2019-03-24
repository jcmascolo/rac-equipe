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
        private readonly RacEquipeServices _racEquipeServices;

        public CreateModel(RacEquipe.Entity.RacDataContext context)
        {
            _context = context;
            _racEquipeServices = new RacEquipeServices(_context);
        }

        public IActionResult OnGet()
        {
            bool premierEssaie = Reservation == null ? true : false;

            ViewData["EquipementId"] = new SelectList(_context.Equipement, "EquipementId", "EquipementId");
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId");
            ViewData["ReservationCompletee"] = premierEssaie ? premierEssaie : Reservation.ReservationCompletee;
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

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
                return OnGet();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}