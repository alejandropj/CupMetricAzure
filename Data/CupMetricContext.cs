﻿using CupMetric.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CupMetric.Data
{
    public class CupMetricContext : DbContext
    {
        public CupMetricContext(DbContextOptions<CupMetricContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Utensilio> Utensilios { get; set; }
    }
}