using CrowdGod.Data;
using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Model.AnswerModel
{
    public class AnswerCreate
    {
        public int AnswerId { get; set; }

        [Display(Name = "Answer:"), MaxLength(2000, ErrorMessage = "Please limit your question to 2000 characters"), DataType(DataType.MultilineText)]
        public string? Content { get; set; }

        [Display(Name = "Answered on:"), DataType(DataType.Date)]
        public DateTimeOffset CreatedUtc { get; set; }

        public int? QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
