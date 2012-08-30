﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
// using System.Data.Entity.Infrastructure;

namespace UtahPlanners.Domain
{
    public partial class PropertiesDB : DbContext
    {
        public PropertiesDB()
            : base("name=PropertiesDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // TODO: look at fixing this or just regenerating the model
            // throw new UnintentionalCodeFirstException();
            throw new Exception("UnintentionalCodeFirstException");
        }
    
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CommonCode> CommonCodes { get; set; }
        public DbSet<EnclosureCode> EnclosureCodes { get; set; }
        public DbSet<NeighborhoodCode> NeighborhoodCodes { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<SocioEconCode> SocioEconCodes { get; set; }
        public DbSet<StreetconnCode> StreetconnCodes { get; set; }
        public DbSet<StreetSafteyCode> StreetSafteyCodes { get; set; }
        public DbSet<StreetType> StreetTypes { get; set; }
        public DbSet<StreetwalkCode> StreetwalkCodes { get; set; }
        public DbSet<Weight> Weights { get; set; }
        public DbSet<PropertiesIndex> PropertiesIndexes { get; set; }
    }
}
