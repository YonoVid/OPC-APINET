using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Model.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public String CreatedBy { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public String UpdatedBy { get; set; } = String.Empty;
        public DateTime? UpdatedDate { get; set; }
        public String DeletedBy { get; set; } = String.Empty;
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
