using AlunosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasKey(c => c.Id);

            modelBuilder.Entity<Aluno>().Property(c => c.Nome)
                                        .HasMaxLength(80)
                                        .IsRequired();

            modelBuilder.Entity<Aluno>().Property(c => c.Email)
                                        .HasMaxLength(100)
                                        .IsRequired();

            modelBuilder.Entity<Aluno>().Property(c => c.Idade)
                                        .IsRequired();

            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Maria do Bairro",
                    Email = "mariadobairro@gmail.com",
                    Idade = 30
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Luis Fernando",
                    Email = "luisfernando@gmail.com",
                    Idade = 33
                }
           );
        }
    }
}
