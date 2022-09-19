using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Model.DataModels
{
    public class Student : BaseEntity
    {

        [Required]
        public String FirstName{ get; set; } = String.Empty;
        [Required]
        public String LastName { get; set; } = String.Empty;
        [Required]
        public DateTime DOB { get; set; }


        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
