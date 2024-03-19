using backend.Models.Apostas;
using backend.Models.Sorteios;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public DbSet<Sorteio> Sorteios { get; set; } = null!;
    public DbSet<Aposta> Apostas { get; set; } = null!;
    public DbSet<GanhadorSorteio> Ganhadores { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sorteio>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd(); 

        modelBuilder.Entity<Sorteio>()
            .HasKey(s => s.Id); 

        modelBuilder.Entity<Aposta>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Aposta>()
            .HasKey(a => a.Id); 

        modelBuilder.Entity<Sorteio>()
            .HasMany(s => s.TodasApostas)
            .WithOne(a => a.Sorteio)
            .HasForeignKey(a => a.SorteioId)
            .IsRequired(); 

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conn_string = "Data Source=db/BancoApostas.db";
        optionsBuilder.UseSqlite(conn_string);
        base.OnConfiguring(optionsBuilder);
    }
}