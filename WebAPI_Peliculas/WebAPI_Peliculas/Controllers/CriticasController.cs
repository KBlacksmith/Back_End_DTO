using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Peliculas.DTOs;
using WebAPI_Peliculas.Entidades;

namespace WebAPI_Peliculas.Controllers
{
    [ApiController]
    [Route("api/peliculas/{peliculaId:int}/criticas")]
    public class CriticasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public CriticasController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<CriticaDTO>>> Get(int peliculaId)
        {
            var existe = await dbContext.Peliculas.AnyAsync(peliculaDB => peliculaDB.Id == peliculaId);
            if (!existe)
            {
                return NotFound();
            }
            var criticas = await dbContext.Criticas.Where(criticasDb => criticasDb.PeliculaId == peliculaId).ToListAsync();
            return mapper.Map<List<CriticaDTO>>(criticas);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int peliculaId, CriticaCreacionDTO criticaCreacionDTO)
        {
            var existe = await dbContext.Peliculas.AnyAsync(peliculaDB => peliculaDB.Id == peliculaId);
            if (!existe)
            {
                return NotFound();
            }
            var critica = mapper.Map<Critica>(criticaCreacionDTO);
            critica.PeliculaId = peliculaId;
            dbContext.Add(critica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
