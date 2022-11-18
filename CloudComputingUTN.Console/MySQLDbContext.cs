using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;
using Microsoft.EntityFrameworkCore;

public partial class MySQLDbContext : BaseDbContext
{
	public MySQLDbContext(DbContextOptions<MySQLDbContext> options) : base(options)
	{

	}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseMySql("server=localhost;database=museumdb;user=nebursoft;pwd=6taFaL7WWayge3e8lgmE", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PRIMARY");

            entity.ToTable("artists");

            entity.Property(e => e.ArtistName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ArtistWikiPage).HasMaxLength(500);
        });

        modelBuilder.Entity<Artwork>(entity =>
        {
            entity.HasKey(e => e.ArtworkId).HasName("PRIMARY");

            entity.ToTable("artworks");

            entity.Property(e => e.ArtworkDescription).HasMaxLength(300);
            entity.Property(e => e.ArtworkName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ArtworkURL)
                .HasMaxLength(1000)
                .HasColumnName("ArtworkURL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}