using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolLogin.Models;

namespace SchoolLogin.Data
{
    public class MockSchoolloginRepo : ISchoolloginRepo
    {
        public void CreateCommand(StudentRoster cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(StudentRoster cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<StudentRoster> GetAllCommands()
        {
            var studentlist = new List<StudentRoster>
            {
                new StudentRoster{ID =0,StudentName ="Stanley",email = "stanley@gmail.com",password=12345},
                new StudentRoster{ID =1,StudentName ="Franco",email = "Franco@gmail.com",password=23456},
                new StudentRoster{ID =2,StudentName ="Lesly",email = "Lesly@gmail.com",password=64324}
            };
            return studentlist;
        }

        public StudentRoster GetCommandById(int id)
        {
            return new StudentRoster{ID =0,StudentName ="Stanley",email = "stanley@gmail.com",password=12345};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(StudentRoster cmd)
        {
            throw new System.NotImplementedException();
        }

        Task ISchoolloginRepo.CreateCommand(StudentRoster cmd)
        {
            throw new System.NotImplementedException();
        }

        Task ISchoolloginRepo.DeleteCommand(StudentRoster cmd)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<StudentRoster>> ISchoolloginRepo.GetAllCommands()
        {
            throw new System.NotImplementedException();
        }

        Task<StudentRoster> ISchoolloginRepo.GetCommandById(int id)
        {
            throw new System.NotImplementedException();
        }

        Task ISchoolloginRepo.UpdateCommand(StudentRoster cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}