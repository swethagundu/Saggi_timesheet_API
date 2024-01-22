using Microsoft.EntityFrameworkCore;
using Saggi_timesheet_API.Models;

namespace Saggi_timesheet_API.Data
{
    public class SaggiTSDbContext : DbContext
    {
        public SaggiTSDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Role>Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Project>  Project { get; set; }
        public DbSet<TimeSheet> TimeSheet { get; set; }
        public DbSet<TimesheetEntry> TimesheetEntry { get; set; }
        public DbSet<Approval> Approval { get; set; }


    }
}
