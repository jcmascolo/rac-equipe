using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RacEquipe.Entity;
using RacEquipe.Models;

namespace RacEquipe
{
    public class RacEquipeServices
    {
        private RacDataContext _racDataContext;
        public RacEquipeServices(RacDataContext racDataContext) => _racDataContext = racDataContext;
        
        public async Task<bool> Reserver(ReservationRequest request)
        {
            var reservationResponse = new ReservationResponse();
            var reservations = await GetReservations(request);
            bool isAvailable = ValidateAvailability(request, reservations);
            if(isAvailable)
            {
                Reservation newReservation = CreateReservation(request);
                try
                {
                    _racDataContext.Reservations
                        .Add(newReservation);
                    await _racDataContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    reservationResponse.IsReserved = false;
                    reservationResponse.ErrorMessage = ex.InnerException.ToString();
                }
            }
            return reservationResponse.IsReserved;
        }

        private Reservation CreateReservation(ReservationRequest request) =>
            new Reservation
            {
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                Equipement = request.Equipement,
                Utilisateur = request.Utilisateur
            };

        private bool ValidateAvailability(ReservationRequest request, List<Reservation> reservations) =>
            reservations
                .Where(x => x.DateTo >= request.DateFrom)
                .Where(x => x.DateFrom <= request.DateTo)
                .Any();

        private async Task<List<Reservation>> GetReservations(ReservationRequest request) =>
            await _racDataContext.Reservations
                .Where(x => x.EquipementId == request.Equipement.EquipementId)
                .ToListAsync();        
    }
}