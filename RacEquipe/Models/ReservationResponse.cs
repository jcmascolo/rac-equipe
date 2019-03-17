using System;
using System.ComponentModel.DataAnnotations;

namespace RacEquipe.Models
{
    class ReservatioResponse
    {
        public bool IsReserved { get; set; }
        public string ErrorMessage { get; set; }
    }
}
