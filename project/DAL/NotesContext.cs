using Microsoft.EntityFrameworkCore;
using project.Models;
using project.Models.Props;

namespace project.DA
{
    public class NotesContext: DbContext
    {
        public DbSet<Note> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Properties properties = new Properties(null);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=userDB; Trusted_Connection=True;");
        }
    }
}
