using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class AddAuthorViewModel
    {
        [Required(ErrorMessage = "Author name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string? Name { get; set; }

        [StringLength(2000, ErrorMessage = "Biography cannot exceed 2000 characters")]
        public string? Biography { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.Url)]
        [StringLength(500, ErrorMessage = "Profile image URL cannot exceed 500 characters")]
        public string? ProfileImageUrl { get; set; }
    }
}
