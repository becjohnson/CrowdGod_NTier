using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Model.ReplyModel
{
    public class ReplyListItem
    {
        public int ReplyId { get; set; }

        [Display(Name = "Reply:"), MaxLength(500, ErrorMessage = "Please limit your question to 2000 characters"), DataType(DataType.MultilineText)]
        public string? Content { get; set; }

        [Display(Name = "Asked on:"), DataType(DataType.Date)]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
