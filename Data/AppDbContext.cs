using Microsoft.EntityFrameworkCore;
using VisitCardEfCore.Models;

namespace VisitCardEfCore.Data
{
    public class AppDbContext : DbContext
    {
        /**
         * Para representar a tabela e realizar as oprações de Create, Delete, Update, Search
         */
        public DbSet<VisitCard> VisitCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
        }
    }
}