using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.DA
{
    public class FileInfoContext: DbContext
    {
        public DbSet<FileInfo> FileInfos { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=userDB; Trusted_Connection=True");
        }
    }
}
