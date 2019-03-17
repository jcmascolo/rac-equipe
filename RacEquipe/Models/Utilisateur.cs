using System;
using System.ComponentModel.DataAnnotations;

namespace RacEquipe.Models
{
    class Utilisateur
    {
        public int UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }
}
