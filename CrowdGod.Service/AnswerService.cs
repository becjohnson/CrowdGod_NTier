using CrowdGod.Data;
using CrowdGod.Model.AnswerModel;
using Microsoft.EntityFrameworkCore;

namespace CrowdGod.Service
{
    public class AnswerService : IAnswerService
    {
        private readonly DbContextOptions<CrowdGodDbContext> _options;
        private readonly CrowdGodDbContext _context;

        public AnswerService(DbContextOptions<CrowdGodDbContext> opts, CrowdGodDbContext ctx)
        {
            _context = ctx;
            _options = opts;
        }

        public async Task<AnswerSortViewModel> GetAllAsync(string searchString)
        {
            var answers = from m in _context.Answers
                          select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                answers = answers.Where(s => (s.Content).Contains(searchString));
            }

            var answerSortVM = new AnswerSortViewModel
            {
                Answers = await answers.Select(s => new AnswerListItem
                {
                    AnswerId = s.AnswerId,
                    Content = s.Content,
                    CreatedUtc = s.CreatedUtc
                }).ToListAsync()
            };
            return answerSortVM;
        }

        public async Task<List<AnswerListItem>> GetAllAsync()
        {
            var answers = from a in _context.Answers
                          select a;

            var answerList = await answers.OrderBy(a => a.CreatedUtc).Select(a => new AnswerListItem
            {
                AnswerId = a.AnswerId,
                Content = a.Content,
                CreatedUtc = a.CreatedUtc
            }).ToListAsync();
            return answerList;
        }

        public async Task<AnswerDetail> Details(int? id)
        {
            var answer = await _context.Answers
                .FirstOrDefaultAsync(m => m.AnswerId == id);

            return new AnswerDetail
            {
                AnswerId = answer.AnswerId,
                Content = answer.Content,
                CreatedUtc = answer.CreatedUtc,
                ModifiedUtc = answer.ModifiedUtc,
            };
        }

        public async Task<bool> Create(int questionid, AnswerCreate answerCreate)
        {
            var answer = new Answer
            {
                QuestionId = questionid,
                Content = answerCreate.Content,
                CreatedUtc = DateTimeOffset.Now
            };
            _context.Add(answer);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<AnswerEdit> Edit(int? id)
        {
            var answer = await _context.Answers.FindAsync(id);

            return new AnswerEdit
            {
                Content = answer.Content,
                ModifiedUtc = new DateTimeOffset(DateTime.Now),
                AnswerId = answer.AnswerId,
            };
        }

        public async Task<bool> Edit(int id, AnswerEdit answerEdit)
        {
            var answer = new Answer
            {
                AnswerId = id,
                Content = answerEdit.Content,
                ModifiedUtc = new DateTimeOffset(DateTime.Now),
            };

            try
            {
                _context.Update(answer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(answerEdit.AnswerId))
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

        public async Task<AnswerDetail> Delete(int? id)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(m => m.AnswerId == id);
            var answerDetail = new AnswerDetail
            {
                Content = answer.Content,
                AnswerId = answer.AnswerId,
                CreatedUtc = answer.CreatedUtc,
            };

            return answerDetail;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answer);
            return await _context.SaveChangesAsync() == 1;
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }
    }
}

