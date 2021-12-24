using Microsoft.EntityFrameworkCore;
using StateManagement.Entities;

namespace StateManagement
{
    public class DataSourceContext : DbContext
    {
        public DataSourceContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FlowEntity> Flows { get; set; }
        public DbSet<StateEntity> States { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskHistoryEntity> TaskHistories { get; set; }
    }
}
