using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Model.AnswerModel
{
    public class AnswerEdit
    {
        public int AnswerId { get; set; }

        [Display(Name = "Answer:"), Required(ErrorMessage = "This field is required"), MaxLength(2000, ErrorMessage = "Please limit your question to 2000 characters"), DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Edited on:"), DataType(DataType.Date)]
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
