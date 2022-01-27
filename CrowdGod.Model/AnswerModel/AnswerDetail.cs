using CrowdGod.Data;
using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Model.AnswerModel
{
    public class AnswerDetail
    {
        public int AnswerId { get; set; }

        [Display(Name = "Answer:"), Required(ErrorMessage = "This field is required"), MaxLength(2000, ErrorMessage = "Please limit your question to 2000 characters"), DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Answered on:"), DataType(DataType.Date)]
        public DateTimeOffset? CreatedUtc { get; set; }

        [Display(Name = "Edited on:"), DataType(DataType.Date)]
        public DateTimeOffset? ModifiedUtc { get; set; }

        public string? QuestionId { get; set; }
        public Question? Question { get; set; }

        public string? ReplyId { get; set; }
        public Reply? Reply { get; set; }
        public IEnumerable<Reply>? Replies { get; set; }
    }
}
