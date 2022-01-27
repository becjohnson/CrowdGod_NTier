using CrowdGod.Data;

namespace CrowdGod.Model.TagModel
{
    public class TagDetail
    {
        public int TagId { get; set; }
        public string? Text { get; set; }
        public IEnumerable<Tag>? Tags { get; set; }

        public string? QuestionId { get; set; }
        public Question? Question { get; set; }
        public IEnumerable<Question>? Questions { get; set; }
    }
}
