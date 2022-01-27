using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Data
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }

        [Display(Name = "Reply:"), MaxLength(500, ErrorMessage = "Please limit your reply to 2000 characters"), DataType(DataType.MultilineText)]
        public string? Content { get; set; }

        [Display(Name = "Asked on:"), DataType(DataType.Date)]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Edited on:"), DataType(DataType.Date)]
        public DateTimeOffset? ModifiedUtc { get; set; }

        public int? AnswerId { get; set; }
        public Answer? Answer { get; set; }
    }
}
