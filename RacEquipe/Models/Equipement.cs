using System;
using System.ComponentModel.DataAnnotations;

namespace RacEquipe.Models
{
    public class Equipement
    {
        public int EquipementId { get; set; }
        public string Description { get; set; }
        public string NumeroDeSerie { get; set; }
    }
}