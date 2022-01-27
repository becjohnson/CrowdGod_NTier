using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Model.QuestionModel
{
    public class QuestionEdit
    {
        public int QuestionId { get; set; }

        [Display(Name = "Title:"), MaxLength(50, ErrorMessage = "Please limit your title to 50 characters"), Required]
        public string Title { get; set; }

        [Display(Name = "Question:"), MaxLength(2000, ErrorMessage = "Please limit your question to 2000 characters"), DataType(DataType.MultilineText)]
        public string? Content { get; set; }

        [Display(Name = "Edited on:"), DataType(DataType.Date)]
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
