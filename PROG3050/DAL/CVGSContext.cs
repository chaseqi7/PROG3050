﻿using PROG3050.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PROG3050.DAL
{
    public class CVGSContext : DbContext
    {
    
        public CVGSContext() : base("CVGSContext")
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }

        public System.Data.Entity.DbSet<PROG3050.Models.Account> Accounts { get; set; }
    }
}