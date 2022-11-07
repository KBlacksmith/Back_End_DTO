using System.ComponentModel.DataAnnotations;

namespace WebAPI_Peliculas.DTOs
{
    public class PeliculaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<CriticaDTO> Criticas { get; set; }
        public List<DirectorDTO> Directores { get; set; }
    }
}
