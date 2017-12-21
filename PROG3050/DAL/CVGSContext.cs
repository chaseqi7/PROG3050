using PROG3050.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PROG3050.DAL
{
    public class CVGSContext : DbContext
    {
    
        public CVGSContext() : base("CVGSContext")
        {
        }

        public DbSet<Usergroup> Usergroup { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }

        public System.Data.Entity.DbSet<PROG3050.Models.Friend> Friends { get; set; }
    }
}