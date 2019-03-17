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
            var reservations = GetReservations(request);
            bool isAvailable = ValidateAvailability(request, reservations);
            if(isAvailable)
            {
                var newReservation = new Reservation
                {
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo,
                    Equipement = request.Equipement,
                    Utilisateur = request.Utilisateur
                };
                try
                {
                    _racDataContext.Reservation
                        .Add(newReservation);
                    await _racDataContext.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    reservationResponse.IsReserved = false;
                    reservationResponse.ErrorMessage = ex.InnerException.ToString();
                }
            }
            return reservationResponse.IsReserved;
        }

        private bool ValidateAvailability(ReservationRequest request, Task<List<Reservation>> reservations) =>
            reservations.Result
                .Where(x => x.DateTo >= request.DateFrom)
                .Where(x => x.DateFrom <= request.DateTo)
                .Any();

        private async Task<List<Reservation>> GetReservations(ReservationRequest request) =>
            _racDataContext.Equipement
                .Include(x => x.Reservation)
                .Where(x => x.idEquipement == request.Equipement.EquipementId)
                .Select(x => x.Reservations)
                .ToListAsync();
        
    }
}