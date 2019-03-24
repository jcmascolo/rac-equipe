using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RacEquipe.Models
{
    internal static class ReservationExtensions
    {
        public static bool ReservationPourMemePlageHoraireExiste(this Reservation reservation, IEnumerable<Reservation> reservations) =>
            reservations
            .Where(r => r.DateFrom <= reservation.DateFrom && r.DateTo >= reservation.DateTo)
            .Any();
    }
}