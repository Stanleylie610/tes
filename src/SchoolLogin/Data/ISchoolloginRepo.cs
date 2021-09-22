using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolLogin.Models;

//i in front is interface
//no implement just define

namespace SchoolLogin.Data
{
    public interface ISchoolloginRepo
    {
        bool SaveChanges();
        Task<IEnumerable<StudentRoster>> GetAllCommands();
        Task<StudentRoster> GetCommandById(int id);
        Task CreateCommand(StudentRoster cmd);
        Task UpdateCommand(StudentRoster cmd);
        Task DeleteCommand(StudentRoster cmd);
    }
}