using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class PulloutBookCopyViewModel
    {
        public Guid BookCopyId { get; set; }

        [Required(ErrorMessage = "Pullout reason is required")]
        [StringLength(500, ErrorMessage = "Pullout reason cannot exceed 500 characters")]
        public string? PulloutReason { get; set; }

        // For display purposes
        public string? BookTitle { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Condition { get; set; }
    }
}
