using AbpFramework.Poc.Core.Entities;
using AbpFramework.Poc.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AbpFramework.Poc.Data
{
    [ConnectionStringName("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Asidb;")]
    public class AsidbContext : AbpDbContext<AsidbContext>
    {
        public AsidbContext(DbContextOptions<AsidbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        //protected AsidbContext(DbContextOptions options)
        //    : base(options)
        //{
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Asidb;");
        }
    }
}
