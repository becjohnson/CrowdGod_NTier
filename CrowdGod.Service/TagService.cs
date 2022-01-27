using CrowdGod.Data;
using Microsoft.EntityFrameworkCore;
using CrowdGod.Model.QuestionModel;

namespace CrowdGod.Service
{
    public class TagService : ITagService
    {
        private readonly DbContextOptions<CrowdGodDbContext> _options;
        private readonly CrowdGodDbContext _context;
        public TagService(DbContextOptions<CrowdGodDbContext> opts, CrowdGodDbContext ctx)
        {
            _context = ctx;
            _options = opts;
        }
        public async Task<QuestionSortViewModel> GetAllAsync(string searchString)
        {
            var tags = from t in _context.Tags
                       select t;

            if (!string.IsNullOrEmpty(searchString))
            {
                tags = tags.Where(t => (t.Text).Contains(searchString));
            }
            var tagSortVM = new QuestionSortViewModel
            {
                Questions = await tags.Select(t => new QuestionListItem
                {
                    Title = t.Questions[0].Title,
                    CreatedUtc = t.Questions[0].CreatedUtc,

                }).ToListAsync()
            };
            return tagSortVM;
        }
    }
}