using Microsoft.EntityFrameworkCore;
using SchoolLogin.Models;

namespace SchoolLogin.Data
{
    public class SchoolLoginContext : DbContext //inherit base class dbcontext
    {
        //define constructor
        //constructor for dependency to be injected
        public SchoolLoginContext(DbContextOptions<SchoolLoginContext> opt) : base(opt)
        {
            
        }

        public DbSet<StudentRoster> studentlist { get; set; }   //represent commandobj(model) as a dbset and make it called studentlist mapping
    }
}