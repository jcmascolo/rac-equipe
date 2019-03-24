using System;
using System.ComponentModel.DataAnnotations;

namespace RacEquipe.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public int EquipementId { get; set; }
        public int UtilisateurId { get; set; }
        internal bool ReservationCompletee { get; set; }

        public Equipement Equipement { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}
