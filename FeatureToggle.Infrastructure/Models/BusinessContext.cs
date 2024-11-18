﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Configurations;
using FeatureToggle.Domain.Entity.BusinessSchema;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Infrastructure.Models
{
    public class BusinessContext : DbContext
    { 
        public DbSet<Business> Business { get; set; }
        public DbSet<Feature> Feature { get; set; }
        public DbSet<BusinessFeatureFlag> BusinessFeatureFlag { get; set; }
        public DbSet<FeatureType> FeatureType { get; set; }

        public BusinessContext(DbContextOptions<BusinessContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BusinessConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessFeatureFlagConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureTypeConfiguration());
        }

    }
}