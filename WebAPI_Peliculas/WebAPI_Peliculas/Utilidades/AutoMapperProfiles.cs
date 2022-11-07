using AutoMapper;
using WebAPI_Peliculas.DTOs;
using WebAPI_Peliculas.Entidades;

namespace WebAPI_Peliculas.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DirectorCreacionDTO, Director>();
            CreateMap<Director, DirectorDTO>();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(pelicula => pelicula.DirectoresPeliculas, opciones => opciones.MapFrom(MapDirectoresPeliculas));
            CreateMap<Pelicula, PeliculaDTO>()
                .ForMember(peliculaDTO => peliculaDTO.Directores, opciones => opciones.MapFrom(MapPeliculaDTODirectores));

            CreateMap<CriticaCreacionDTO, Critica>();
            CreateMap<Critica, CriticaDTO>();
        }

        private List<DirectorDTO> MapPeliculaDTODirectores(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<DirectorDTO>();
            if(pelicula.DirectoresPeliculas == null)
            {
                return resultado;
            }
            foreach(var directorPelicula in pelicula.DirectoresPeliculas)
            {
                resultado.Add(new DirectorDTO()
                {
                    Id = directorPelicula.DirectorId,
                    Nombre = directorPelicula.Director.Nombre
                });
            }
            return resultado;
        }
        private List<DirectorPelicula> MapDirectoresPeliculas(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<DirectorPelicula>();
            if (peliculaCreacionDTO == null)
            {
                return resultado;
            }
            foreach(var directorId in peliculaCreacionDTO.DirectoresId)
            {
                resultado.Add(new DirectorPelicula() { DirectorId = directorId });
            }
            return resultado;
        }
    }
}
