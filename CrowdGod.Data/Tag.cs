using System.ComponentModel.DataAnnotations;

namespace CrowdGod.Data
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string? Text { get; set; }

        public IList<Question>? Questions { get; } = new List<Question>();
        public IList<QuestionTag>? QuestionTags { get; } = new List<QuestionTag>();
    }
}
