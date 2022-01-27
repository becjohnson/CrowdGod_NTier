using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CrowdGod.Model.QuestionModel;
using CrowdGod.Data;

namespace CrowdGod.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly DbContextOptions<CrowdGodDbContext> _options;
        private readonly CrowdGodDbContext _context;
        public QuestionService(DbContextOptions<CrowdGodDbContext> opts, CrowdGodDbContext ctx)
        {
            _context = ctx;
            _options = opts;
        }

        public async Task<QuestionSortViewModel> GetAllAsync(string searchString)
        {
            var questions = from q in _context.Questions
                            select q;

            if (!string.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(q => q.Content.Contains(searchString) || q.Title.Contains(searchString));
            }

            var questionSortVM = new QuestionSortViewModel
            {
                Questions = await questions.Select(q => new QuestionListItem
                {
                    QuestionId = q.QuestionId,
                    Title = q.Title,
                    CreatedUtc = q.CreatedUtc,
                }).ToListAsync()
            };
            return questionSortVM;
        }

        public async Task<List<QuestionListItem>> GetAllAsync()
        {
            var questions = from q in _context.Questions
                            select q;

            var questionList = await questions.OrderBy(q => q.CreatedUtc).Select(q => new QuestionListItem
            {
                QuestionId = q.QuestionId,
                Title = q.Title,
                CreatedUtc = q.CreatedUtc,
            }).ToListAsync();
            return questionList;
        }

        public async Task<QuestionDetail> Details(int? id)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.QuestionId == id);

            return new QuestionDetail
            {
                QuestionId = question.QuestionId,
                Title = question.Title,
                Content = question.Content,
                CreatedUtc = question.CreatedUtc,
                ModifiedUtc = question.ModifiedUtc,
            };
        }

        public async Task<bool> Create(QuestionCreate questionCreate)
        {
            var cnt = questionCreate.Content;
            if (questionCreate.Content != null)
            {
                await AddHashTags(cnt);
            }
            var tags = ListTags(cnt);
            var question = new Question
            {
                Title = questionCreate.Title,
                Content = questionCreate.Content,
                CreatedUtc = new DateTimeOffset(DateTime.Now),
                Tags = tags,
            };
            _context.Add(question);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<QuestionEdit> Edit(int? id)
        {
            var question = await _context.Questions.FindAsync(id);

            return new QuestionEdit
            {
                QuestionId = question.QuestionId,
                Title = question.Title,
                Content = question.Content,
            };
        }

        public async Task<bool> Edit(int id, QuestionEdit questionEdit)
        {
            var question = new Question
            {
                QuestionId = id,
                Title = questionEdit.Title,
                Content = questionEdit.Content,
                ModifiedUtc = new DateTimeOffset(DateTime.Now)
            };

            try
            {
                _context.Update(question);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(questionEdit.QuestionId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
        public async Task<QuestionDetail> Delete(int? id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(m => m.QuestionId == id);
            var questionDetail = new QuestionDetail
            {
                QuestionId = question.QuestionId,
                Content = question.Content,
                CreatedUtc = question.CreatedUtc,
                ModifiedUtc = question.ModifiedUtc,
            };

            return questionDetail;
        }
        public async Task<bool> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            return await _context.SaveChangesAsync() == 1;
        }

        bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }

        public static List<string> Hashtagger(string content)
        {
            if (content != null)
            {
                var rx = new Regex("#+[a-zA-Z0-9(_)]{1,}", RegexOptions.Compiled);
                MatchCollection matches = rx.Matches(content);
                var list = matches.Cast<Match>().Select(match => match.Value).ToList();
                return list;
            }
            return null;
        }

        public static int HashtagCounter(string content)
        {
            if (content != null)
            {
                var rx = new Regex("#+[a-zA-Z0-9(_)]{1,}", RegexOptions.Compiled);
                MatchCollection matches = rx.Matches(content);
                int count = matches.Count;
                return count;
            }
            else
                return 0;
        }

        public List<Tag> ListTags(string content)
        {
            List <Tag> tags = new List<Tag>();
            int count = HashtagCounter(content);
            List<string> hashTags = Hashtagger(content);
            foreach (string item in hashTags)
            {
                hashTags.Select(s => s[0..^0].Distinct());
                Tag hashTag = new() { Text = item };
                tags.Add(hashTag);
            }
            return tags;
        }

        public async Task<bool> AddHashTags(string content)
        {
            int count = HashtagCounter(content);
            List<string> hashTags = Hashtagger(content);
            foreach (string item in hashTags)
            {
                hashTags.Select(s => s[0..^0].Distinct());
                Tag hashTag = new() { Text = item };
                _context.Tags.Add(hashTag);
            }
            return await _context.SaveChangesAsync() == count;
        }
    }
}
