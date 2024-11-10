using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_new.Models;
using System.Net.Sockets;

namespace project_new.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Captain> Captain { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Stadium> Stadium { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.NoAction); // or DeleteBehavior.Cascade if appropriate

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.NoAction); // or DeleteBehavior.Cascade if appropriate

            //var admin = new IdentityRole("Admin");
            //admin.NormalizedName = "Admin";

            //var client = new IdentityRole("Client");
            //admin.NormalizedName = "Client";

            //modelBuilder.Entity<IdentityRole>().HasData(admin, client);


        }
    }

}
