using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RacEquipe.Models;

namespace RacEquipe.Entity
{
    public class RacDataContext : DbContext
    {
        public RacDataContext(DbContextOptions<RacDataContext> options) : base(options)
        {
        }

        public DbSet<Equipement> Equipement { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}