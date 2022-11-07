using System.ComponentModel.DataAnnotations;
using WebAPI_Peliculas.Validaciones;

namespace WebAPI_Peliculas.DTOs
{
    public class DirectorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener máximo 100 caracteres")]
        [PrimeraLetraMayuscula(ErrorMessage = "Lol")]
        public string Nombre { get; set; }
    }
}
