using CupMetric.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CupMetric.Data
{
    public class CupMetricContext : DbContext
    {
        public CupMetricContext(DbContextOptions<CupMetricContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
