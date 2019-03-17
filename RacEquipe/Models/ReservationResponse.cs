﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RacEquipe.Models
{
    public class ReservationResponse
    {
        public bool IsReserved { get; set; }
        public string ErrorMessage { get; set; }
    }
}
