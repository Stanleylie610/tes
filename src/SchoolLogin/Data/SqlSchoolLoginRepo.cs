using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolLogin.Models;

namespace SchoolLogin.Data
{
    public class SqlSchoolLoginRepo : ISchoolloginRepo
    {
        private readonly SchoolLoginContext _context;

        public SqlSchoolLoginRepo(SchoolLoginContext context)
        {
            _context = context;
        }

        public async Task CreateCommand(StudentRoster cmd)
        {
            await _context.studentlist.AddAsync(cmd);
        }

        public async Task DeleteCommand(StudentRoster cmd)
        {
           _context.studentlist.Remove(cmd);       // It can't be asynchronous
        }

        public async Task<IEnumerable<StudentRoster>> GetAllCommands()
        {
            return await _context.studentlist.ToListAsync();
        }

        public async Task<StudentRoster> GetCommandById(int id)
        {
            return await _context.studentlist.SingleOrDefaultAsync(p=>p.ID == id );
        }

        public bool SaveChanges()
        {
            return(_context.SaveChanges()>=0);
        }

        public async Task UpdateCommand(StudentRoster cmd)
        {
            
        }

    }
}