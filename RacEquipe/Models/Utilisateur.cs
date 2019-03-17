using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RacEquipe.Models
{
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
