using CrowdGod.Data;
using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Model.QuestionModel
{
    public class QuestionCreate
    {
        public int QuestionId { get; set; }

        [Display(Name = "Title:"), MaxLength(50, ErrorMessage = "Please limit your title to 50 characters"), Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Display(Name = "Question:"), MaxLength(2000, ErrorMessage = "Please limit your question to 2000 characters"), DataType(DataType.MultilineText)]
        public string? Content { get; set; }

        [Display(Name = "Asked on:"), DataType(DataType.Date)]
        public DateTimeOffset CreatedUtc { get; set; }

        //Foreach inside of the modelcreate method
        public string? TagId { get; set; }
        public Tag? Tag { get; set; }
        public IList<Tag>? Tags { get; set;  } = new List<Tag>();
    }
}
