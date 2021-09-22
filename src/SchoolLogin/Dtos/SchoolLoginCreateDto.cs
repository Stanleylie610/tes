using System.ComponentModel.DataAnnotations;

namespace SchoolLogin.Dtos  //name of the project . name of the folder
{
    //public record schoologincreatedto for .Net 5.0
    public class SchoolLoginCreateDto
    {   
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public int password { get; set; }
    }
}