using System;
using System.ComponentModel.DataAnnotations;

namespace RacEquipe.Models
{
    class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public int EquipementId { get; set; }
        public int UtilisateurId { get; set; }
    }
}
