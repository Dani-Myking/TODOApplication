using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOApplication.Models;
using TODOApplication.Models.AppDbContext.DbContexts;

namespace TODOApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KategoriController : ControllerBase
    {

        private readonly AppDbContext dbContext;

        [FromQuery(Name = "id")]
        public int? queryId { get; set; }

        public KategoriController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllKategori()
        {

            var Kategori = dbContext.Kategori
                 .Select(k => k)
                 .ToListAsync();

            return Ok(await Kategori);

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateKategori(KategoriCreateDTO dto)
        {

            if (dto == null)
                return BadRequest();

            var Kategori = new Kategori(dto);
            await dbContext.Kategori.AddAsync(Kategori);
            await dbContext.SaveChangesAsync();

            return Ok();

        }

        [HttpGet("view")]
        public async Task<IActionResult> GetKategoriById()
        {

            if (queryId == null)
            {
                return BadRequest();
            }
            else
            {
                var TODO = dbContext.TODO
                    .Where(t => t.Id == queryId)
                    .Select(t => t)
                    .FirstOrDefaultAsync();

                return Ok(await TODO);
            }

        }
    }
}