using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Model.DataModels
{
    public class Chapters : BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course();
        [Required]
        public String List { get; set; } = String.Empty;
    }
}
