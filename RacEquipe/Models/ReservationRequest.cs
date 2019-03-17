using System;
using System.ComponentModel.DataAnnotations;

namespace RacEquipe.Models
{
    class ReservationRequest
    {
        public Equipement Equipement { get; set; }

        public Utilisateur Utilisateur { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
