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
                    DateFrom = request.dateFrom,
                    DateTo = request.dateTo,
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
                    reservationResponse.IsReverved = false;
                    reservationResponse.ErrorMessage = ex.InnerException.ToString();
                }
            }
            return reservationResponse;
        }

        private static bool ValidateAvailability(ReservationRequest request, Task<List<Reservation>> reservations)
        {
            return reservations.Result
                .Where(x => x.dateTo >= request.dateFrom)
                .Where(x => x.dateFrom <= request.dateTo)
                .Any();
        }

        private async Task<List<Reservation>> GetReservations(ReservationRequest request) =>
            _racDataContext.Equipement
                .Include(x => x.Reservation)
                .Where(x => x.idEquipement == request.Equipement.idEquipement)
                .Select(x => x.Reservations)
                .ToListAsync();
        }
    }
}