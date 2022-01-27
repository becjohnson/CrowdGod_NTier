using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Model.QuestionModel
{
    public class QuestionListItem
    {
        [Key]
        public int QuestionId { get; set; }

        [Display(Name = "Title:"), MaxLength(50, ErrorMessage = "Please limit your title to 50 characters"), Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Display(Name = "Asked on:"), DataType(DataType.Date)]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
