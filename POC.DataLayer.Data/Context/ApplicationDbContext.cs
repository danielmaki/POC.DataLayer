using Microsoft.EntityFrameworkCore;

using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructors

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion

        #region Migration info

        public const int SchemeVersion = 0;

        public static readonly string[] SQLMigrationScriptsNS = new[] { "POC.DataLayer.MigrationScripts.SQLMigrations" };

        #endregion

        #region Properties

        /// <summary>Fruit description</summary>
        public DbSet<FruitEntity> Fruits { get; set; }

        #endregion

        #region Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FruitConfiguration());
        }

        #endregion
    }
}
