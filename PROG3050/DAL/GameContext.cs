using PROG3050.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PROG3050.DAL
{
    public class GameContext : DbContext
    {
    
        public GameContext() : base("CVGSContext")
        {
        }

    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
}
}