using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UtahPlanners.POC.Domain;

namespace UtahPlanners.POC.Repository.Mappings
{
    public class PropertyContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }

        public PropertyContext()
            : base("PropertiesConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var prop = modelBuilder.Entity<Property>();
            
            prop.HasKey<Guid>(p => p.Id);
            prop.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            prop.Property(p => p.Id).HasColumnType("uniqueidentifier");

            base.OnModelCreating(modelBuilder);
        }

    }
}