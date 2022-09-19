using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Model.DataModels
{
    public class Category : BaseEntity
    {
        [Required]
        public String Name { get; set; } = String.Empty;
        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
