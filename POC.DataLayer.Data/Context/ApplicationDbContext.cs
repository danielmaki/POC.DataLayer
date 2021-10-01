using System.Linq;

using Microsoft.EntityFrameworkCore;
using POC.DataLayer.Data.Context.Abstractions;

namespace POC.DataLayer.Data.Context
{
    public partial class ApplicationDbContext : DbContext
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

        #region Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Find each ENTITY configuration property and invoke the ApplyConfig method for applying the ENTITY configuration to the model builder
            var properties = GetType().GetProperties();
            foreach (var config in properties.Where(property => typeof(IDbContextEntityConfig).IsAssignableFrom(property.PropertyType)))
            {
                ((IDbContextEntityConfig)config.GetValue(this)).ApplyConfig(modelBuilder);
            }
        }

        #endregion
    }
}
