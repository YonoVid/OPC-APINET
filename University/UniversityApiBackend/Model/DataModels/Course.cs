using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Model.DataModels
{
    public enum CourseLevel { BASIC, INTERMEDIATE, ADVANCED }
    public class Course : BaseEntity
    {
        [Required, StringLength(280)]
        public String Name { get; set; } = String.Empty;

        [Required, StringLength(280)]
        public String Description { get; set; } = String.Empty;

        [Required]
        public String LongDescription { get; set; } = String.Empty;

        [Required]
        public String TargetPublic { get; set; } = String.Empty;

        [Required]
        public String Objectives { get; set; } = String.Empty;

        [Required]
        public String Requirements { get; set; } = String.Empty;


        [Required]
        public CourseLevel Level { get; set; } = CourseLevel.BASIC;
    }
}
