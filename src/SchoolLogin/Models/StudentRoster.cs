using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolLogin.Models  //name of the project . name of the folder
{
    public class StudentRoster
    {

        [Key]   //data anatation
        public int ID { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public int password { get; set; }
    }
}