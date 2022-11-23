using Microsoft.AspNetCore.Identity;

namespace WebAPI_Peliculas.Entidades
{
    public class Critica
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}
