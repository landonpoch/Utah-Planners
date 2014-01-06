using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Infrastructure.DAO
{
    public class PropertyContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<PictureMetaData> PictureMetaData { get; set; }
        public DbSet<Weights> Weights { get; set; }
        public DbSet<PropertyType> PropertyTypeLookups { get; set; }
        public DbSet<StreetType> StreetTypeLookups { get; set; }
        public DbSet<SocioEcon> SocioEconLookups { get; set; }
        public DbSet<StreetSafety> StreetSafetyLookups { get; set; }
        public DbSet<BuildingEnclosure> BuildingEnclosureLookups { get; set; }
        public DbSet<CommonAreas> CommonAreaLookups { get; set; }
        public DbSet<StreetConnectivity> StreetConnectivityLookups { get; set; }
        public DbSet<StreetWalkability> StreetWalkabilityLookups { get; set; }
        public DbSet<NeighborhoodCondition> NeighborhoodConditionLookups { get; set; }

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
