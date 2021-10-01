using Microsoft.EntityFrameworkCore;

namespace POC.DataLayer.Data.Context.Abstractions
{
    public interface IDbContextEntityConfig
    {
        public void ApplyConfig(ModelBuilder modelBuilder);
    }
}
