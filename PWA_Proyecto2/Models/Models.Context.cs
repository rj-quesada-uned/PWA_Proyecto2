﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PWA_Proyecto2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbModels : DbContext
    {
        public DbModels()
            : base("name=DbModels")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despegue>().Property(d => d.NombrePiloto).HasMaxLength(255);

            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Aterrizaje> Aterrizaje { get; set; }
        public virtual DbSet<Aviones> Aviones { get; set; }
        public virtual DbSet<Despegue> Despegue { get; set; }
        public virtual DbSet<MarcasAviones> MarcasAviones { get; set; }
        public virtual DbSet<ModelosAviones> ModelosAviones { get; set; }
        public virtual DbSet<RetiroAviones> RetiroAviones { get; set; }
    }
}
