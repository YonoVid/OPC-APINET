using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Model.DataModels
{
    public enum UserRole { ADMINISTRATOR = 0, USER = 1 }
    public class User : BaseEntity
    {
        [Required, StringLength(50)]
        public String Name { get; set; } = String.Empty;

        [Required, StringLength(100)]
        public String LastName { get; set; } = String.Empty;

        [Required, EmailAddress]
        public String Email { get; set; } = String.Empty;

        [Required]
        public String Password { get; set; } = String.Empty;

        [Required]
        public UserRole Role { get; set; } = UserRole.USER;


    }
}
