using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWorkDemo.Models;

namespace MyWorkDemo.Data
{
    public class MyWorkDbContext : DbContext
    {
        public MyWorkDbContext(DbContextOptions<MyWorkDbContext> options)
            : base(options)
        {

        }

        public DbSet<Activity> Activity { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ActivityUser> ActivityUser { get; set; }
        public DbSet<Note> Note { get; set; }
        
    }
}
