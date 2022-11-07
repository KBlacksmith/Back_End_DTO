using System.ComponentModel.DataAnnotations;

namespace WebAPI_Peliculas.DTOs
{
    public class PeliculaCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Titulo { get; set; }
        public List<int> DirectoresId { get; set; }
    }
}
