using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Swimmer>()
            .HasIndex(a => a.UserName)
            .IsUnique();

            // Many-to-many: Session <-> Attendee
            modelBuilder.Entity<SessionSwimmer>()
                .HasKey(ca => new { ca.SessionId, ca.SwimmerId });

            // Many-to-many: Speaker <-> Session
            modelBuilder.Entity<SessionCoach>()
                .HasKey(ss => new { ss.SessionId, ss.CoachId });
        }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Coach> Coaches { get; set; }

        public DbSet<Swimmer> Swimmers { get; set; }
    }
}
