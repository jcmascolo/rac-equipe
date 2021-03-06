﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RacEquipe.Entity;

namespace RacEquipe.Migrations
{
    [DbContext(typeof(RacDataContext))]
    partial class RacDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("RacEquipe.Models.Equipement", b =>
                {
                    b.Property<int>("EquipementId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("NumeroDeSerie");

                    b.HasKey("EquipementId");

                    b.ToTable("Equipement");
                });

            modelBuilder.Entity("RacEquipe.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int>("EquipementId");

                    b.Property<int>("UtilisateurId");

                    b.HasKey("ReservationId");

                    b.HasIndex("EquipementId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RacEquipe.Models.Utilisateur", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom");

                    b.Property<string>("Prenom");

                    b.HasKey("UtilisateurId");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("RacEquipe.Models.Reservation", b =>
                {
                    b.HasOne("RacEquipe.Models.Equipement", "Equipement")
                        .WithMany("Reservations")
                        .HasForeignKey("EquipementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RacEquipe.Models.Utilisateur", "Utilisateur")
                        .WithMany("Reservations")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
