using Microsoft.EntityFrameworkCore;
using WebAPI_Peliculas.Entidades;

namespace WebAPI_Peliculas
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DirectorPelicula>().HasKey(dp => new {dp.DirectorId, dp.PeliculaId});
        }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Director> Directores { get; set; }
        public DbSet<Critica> Criticas { get; set; }
        public DbSet<DirectorPelicula> DirectoresPeliculas { get; set; }
    }
}
