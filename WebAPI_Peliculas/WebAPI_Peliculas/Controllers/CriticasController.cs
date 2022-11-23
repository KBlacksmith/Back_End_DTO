using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> userManager;
        public CriticasController(ApplicationDbContext dbContext, IMapper mapper, 
            UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int peliculaId, CriticaCreacionDTO criticaCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;
            var existe = await dbContext.Peliculas.AnyAsync(peliculaDB => peliculaDB.Id == peliculaId);
            if (!existe)
            {
                return NotFound();
            }
            var critica = mapper.Map<Critica>(criticaCreacionDTO);
            critica.PeliculaId = peliculaId;
            critica.UsuarioId = usuarioId;
            dbContext.Add(critica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
