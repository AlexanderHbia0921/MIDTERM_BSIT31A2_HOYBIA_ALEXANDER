using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class AddCopyViewModel
    {
        [Required(ErrorMessage = "Book ID is required.")]
        public Guid BookId { get; set; }  

        [Required(ErrorMessage = "Cover image URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string CoverImageUrl { get; set; }  

        [Required(ErrorMessage = "Condition is required.")]
        public string Condition { get; set; }  

        [Required(ErrorMessage = "Source is required.")]
        public string Source { get; set; }  
    }
}
