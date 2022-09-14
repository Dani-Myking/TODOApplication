using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace TODOApplication.Models
{

    namespace AppDbContext.DbContexts
    {
        public class AppDbContext : DbContext
        {

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }

            public DbSet<TODO> TODO { get; set; }
            public DbSet<Kategori> Kategori { get; set; }

        }

    }

}
