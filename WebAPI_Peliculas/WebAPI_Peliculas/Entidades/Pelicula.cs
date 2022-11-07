using System.ComponentModel.DataAnnotations;

namespace WebAPI_Peliculas.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Titulo { get; set; }
        public List<Critica> Criticas { get; set; }
        public List<DirectorPelicula> DirectoresPeliculas { get; set; }
    }
}
