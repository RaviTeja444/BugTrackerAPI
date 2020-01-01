using BugTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Data
{
    public class BugTrackerDbContext : DbContext
    {
        public BugTrackerDbContext(string nameOrConnectionString) : base("name=SchoolDBConnectionString")
        {
            Database.SetInitializer<BugTrackerDbContext>(new CreateDatabaseIfNotExists<BugTrackerDbContext>());

            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }

        public DbSet<Student> student;
    }
}
