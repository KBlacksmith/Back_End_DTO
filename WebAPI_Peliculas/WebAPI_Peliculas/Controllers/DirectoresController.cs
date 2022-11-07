using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Peliculas.Entidades;
using WebAPI_Peliculas.Filtros;
using Microsoft.AspNetCore.Authorization;
using WebAPI_Peliculas.DTOs;
using AutoMapper;

namespace WebAPI_Peliculas.Controllers
{
    [ApiController]
    [Route("api/directores")]
    public class DirectoresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public DirectoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<DirectorDTO>>> Get()
        {
            var directores = await dbContext.Directores.ToListAsync();
            return mapper.Map<List<DirectorDTO>>(directores);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DirectorDTO>> Get(int id)
        {
            var director = await dbContext.Directores.FirstOrDefaultAsync(x => x.Id == id);
            if(director == null)
            {
                return NotFound();
            }
            return mapper.Map<DirectorDTO>(director);
        }
        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<DirectorDTO>>> Get([FromRoute] string nombre)
        {
            var directores = await dbContext.Directores.Where(directorBD => directorBD.Nombre.Contains(nombre)).ToListAsync();
            return mapper.Map<List<DirectorDTO>>(directores);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DirectorCreacionDTO directorCreacionDTO)
        {
            var existeDirector = await dbContext.Directores.AnyAsync(x => x.Nombre == directorCreacionDTO.Nombre);
            if (existeDirector)
            {
                return BadRequest("Ya existe un director con ese nombre");
            }
            var director = mapper.Map<Director>(directorCreacionDTO);
            dbContext.Add(director);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Director director, int id)
        {
            var exists = await dbContext.Directores.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("El director especificado no existe");
            }
            if(director.Id != id)
            {
                return BadRequest("El id del director no coincide con el establecido en la url");
            }
            dbContext.Update(director);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await dbContext.Directores.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("No se encontró el director con id "+id.ToString());
            }
            dbContext.Remove(new Director { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
