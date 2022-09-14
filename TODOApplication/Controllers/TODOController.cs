using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOApplication.Models;
using TODOApplication.Models.AppDbContext.DbContexts;

namespace TODOApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TODOController : ControllerBase
    {

        private readonly AppDbContext dbContext;

        [FromQuery(Name = "id")]
        public int? queryId { get; set; }

        public TODOController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        //search for multiple categories
        [HttpGet("{tittel}")]
        public async Task<IActionResult> GetTODOByTittel(string tittel)
        {

            var TODO = dbContext.TODO
                .Select(t => t.Tittel == tittel)
                .FirstOrDefaultAsync();

            return Ok(await TODO);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTODO()
        {

            var TODO = dbContext.TODO
                 .Include(t => t.Kategori)
                 .Select(t => t)
                 .ToListAsync();

            return Ok(await TODO);

        }

        [HttpGet("view")]
        public async Task<IActionResult> GetTODOById()
        {

            if (queryId == null)
            {
                return BadRequest();
            }
            else
            {
                var TODO = dbContext.TODO
                    .Include(t => t.Kategori)
                    .Where(t => t.Id == queryId)
                    .Select(t => t)
                    .FirstOrDefaultAsync();

                return Ok(await TODO);
            }

        }
        /*
        [HttpGet]
        public async Task<IActionResult> GetAllTODO()
        {

            var TODO = dbContext.TODO
                .Select(t => t)
                .ToListAsync();

            return Ok(await TODO);
        }*/

        [HttpPost("create")]
        public async Task<IActionResult> CreateTODO(TODOCreateDTO dto)
        {

            if (dto == null)
                return BadRequest();
            else
            {

                TODO todo = new TODO(dto);

                var kategori = await dbContext.Kategori
                    .Select(k => k)
                    .Where(k => k.Id == dto.Kategori.Id)
                    .FirstOrDefaultAsync();
                
                todo.Opprettelse = System.DateTime.Now;

                if (kategori == null)
                    return BadRequest();

                todo.Kategori = kategori;

                await dbContext.TODO.AddAsync(todo);
                await dbContext.SaveChangesAsync();

                return Ok(dto);
            }

        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateTODO(TODOUpdateDTO dto)
        {

            var todo = await dbContext.TODO
                .Include(t => t.Kategori)
                .Where(t => t.Id == dto.Id)
                .FirstOrDefaultAsync();

            if (todo == null)
                return BadRequest();
            else
            {

                //todo.Tittel = dto.Tittel;
                //todo.Frist = dto.Frist;
                //todo.Utfort = dto.Utfort;
                //dbContext.Entry(dbContext.TODO.FirstOrDefaultAsync(t => t.Id == dto.Id)).CurrentValues.SetValues(new TODO(dto));
                //var todoEntity = ;
                DateTime opprettet = todo.Opprettelse;
                dbContext.Entry(todo).CurrentValues.SetValues(new TODO(dto));
                todo.Opprettelse = opprettet;
                //todoEntity.State = EntityState.Modified;

                int h = 0;

                var kategori = await dbContext.Kategori
                    .Select(k => k)
                    .Where(k => k.Id == dto.Kategori.Id)
                    .FirstOrDefaultAsync();

                if (kategori == null)
                    return BadRequest();

                todo.Kategori = kategori;

                await dbContext.SaveChangesAsync();
                return Ok();
            }

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTODO()
        {

            var todo = await dbContext.TODO
                .Where(t => t.Id == queryId)
                .FirstOrDefaultAsync();

            if (todo == null)
                return BadRequest();
            else {

                dbContext.TODO.Remove(todo);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

        }
    }
}