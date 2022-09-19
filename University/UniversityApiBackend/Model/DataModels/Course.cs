using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Model.DataModels
{
    public enum CourseLevel { BASIC, INTERMEDIATE, ADVANCED, EXPERT }
    public class Course : BaseEntity
    {
        [Required, StringLength(280)]
        public String Name { get; set; } = String.Empty;

        [Required, StringLength(280)]
        public String ShortDescription { get; set; } = String.Empty;

        [Required]
        public String Description { get; set; } = String.Empty;

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public ICollection<Student> Students{ get; set; } = new List<Student>();

        [Required]
        public Chapters Chapters { get; set; } = new Chapters();

        [Required]
        public CourseLevel Level { get; set; } = CourseLevel.BASIC;
    }
}
