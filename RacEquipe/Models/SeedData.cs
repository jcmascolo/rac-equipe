using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RacEquipe.Entity;
using System;
using System.Linq;

namespace RacEquipe.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RacDataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RacDataContext>>()))
            {
                if (context.Equipement.Any()) return;
                context.Equipement.AddRange(
                    new Equipement
                    {
                        Description = "Equipement 1",
                        NumeroDeSerie = "1XG743JI"
                    },
                    new Equipement
                    {
                        Description = "Equipement 2",
                        NumeroDeSerie = "1XG744JI"
                    },
                    new Equipement
                    {
                        Description = "Equipement 3",
                        NumeroDeSerie = "1XG745JI"
                    },
                    new Equipement
                    {
                        Description = "Equipement 4",
                        NumeroDeSerie = "1XG746JI"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}