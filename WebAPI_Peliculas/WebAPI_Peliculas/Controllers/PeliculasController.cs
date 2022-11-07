using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Peliculas.Entidades;
using WebAPI_Peliculas.DTOs;
using AutoMapper;

namespace WebAPI_Peliculas.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public PeliculasController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculaDTO>>> Get()
        {
            var peliculas =  await dbContext.Peliculas.ToListAsync();
            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id)
        {
            var pelicula = await dbContext.Peliculas.Include(peliculaDb => peliculaDb.DirectoresPeliculas)
                .ThenInclude(directorPeliculaDB => directorPeliculaDB.Director).Include(peliculaDb => peliculaDb.Criticas).FirstOrDefaultAsync(x => x.Id == id);
            pelicula.DirectoresPeliculas = pelicula.DirectoresPeliculas.OrderBy(x => x.Orden).ToList();
            return mapper.Map<PeliculaDTO>(pelicula);
        }
        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreacionDTO peliculaCreacionDTO)
        {
            if(peliculaCreacionDTO.DirectoresId == null || peliculaCreacionDTO.DirectoresId.Count == 0)
            {
                return BadRequest("No se puede crear una película sin directores");
            }


            var directoresIds = await dbContext.Directores
                .Where(directorDB => peliculaCreacionDTO.DirectoresId.Contains(directorDB.Id))
                .Select(x => x.Id).ToListAsync();

            if(peliculaCreacionDTO.DirectoresId.Count != directoresIds.Count)
            {
                return BadRequest("No existe al menos uno de los directores enviados");
            }

            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);

            if(pelicula.DirectoresPeliculas != null)
            {
                for (int i = 0; i < pelicula.DirectoresPeliculas.Count; i++){
                    pelicula.DirectoresPeliculas[i].Orden = i;
                }
            }

            dbContext.Add(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]//api/peliculas/1
        public async Task<ActionResult> Put(Pelicula pelicula, int id)
        {
            var exists = await dbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("La pelicula especificado no existe");
            }
            if (pelicula.Id != id)
            {
                return BadRequest("El id de la película no coincide con el establecido en la url");
            }
            dbContext.Update(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await dbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("No se encontró una película con id "+id.ToString());
            }
            dbContext.Remove(new Pelicula()
            {
                Id = id,
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
