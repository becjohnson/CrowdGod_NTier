using CrowdGod.Data;
using CrowdGod.Model.ReplyModel;
using Microsoft.EntityFrameworkCore;

namespace CrowdGod.Service
{
    public class ReplyService : IReplyService
    {
        private readonly DbContextOptions<CrowdGodDbContext> _options;
        private readonly CrowdGodDbContext _context;
        public ReplyService(DbContextOptions<CrowdGodDbContext> opts, CrowdGodDbContext ctx)
        {
            _context = ctx;
            _options = opts;
        }

        public async Task<ReplySortViewModel> GetAllAsync(string searchString)
        {
            var replies = from m in _context.Replies
                          select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                replies = replies.Where(s => (s.Content).Contains(searchString));
            }

            var replySortVM = new ReplySortViewModel
            {
                Replies = await replies.Select(s => new ReplyListItem
                {
                    ReplyId = s.ReplyId,
                    Content = s.Content,
                    CreatedUtc = s.CreatedUtc
                }).ToListAsync()
            };
            return replySortVM;
        }

        public async Task<ReplySortViewModel> GetAllAsync()
        {
            var replies = from m in _context.Replies
                          select m;

            var replySortVM = new ReplySortViewModel
            {
                Replies = await replies.Select(s => new ReplyListItem
                {
                    ReplyId = s.ReplyId,
                    Content = s.Content,
                    CreatedUtc = s.CreatedUtc
                }).ToListAsync()
            };
            return replySortVM;
        }

        public async Task<ReplyDetail> Details(int? id)
        {
            var reply = await _context.Replies
                .FirstOrDefaultAsync(m => m.ReplyId == id);

            return new ReplyDetail
            {
                ReplyId = reply.ReplyId,
                Content = reply.Content,
                CreatedUtc = reply.CreatedUtc,
                ModifiedUtc = reply.ModifiedUtc,
            };
        }

        public async Task<bool> Create(int answerId, ReplyCreate replyCreate)
        {
            var reply = new Reply
            {
                AnswerId = answerId,
                Content = replyCreate.Content,
                CreatedUtc = DateTimeOffset.Now
            };
            _context.Add(reply);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<ReplyEdit> Edit(int? id)
        {
            var reply = await _context.Replies.FindAsync(id);

            return new ReplyEdit
            {
                Content = reply.Content,
                ModifiedUtc = new DateTimeOffset(DateTime.Now),
                ReplyId = reply.ReplyId,
            };
        }

        public async Task<bool> Edit(int id, ReplyEdit replyEdit)
        {
            var reply = new Reply
            {
                ReplyId = id,
                Content = replyEdit.Content,
                ModifiedUtc = new DateTimeOffset(DateTime.Now),
            };

            try
            {
                _context.Update(reply);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(replyEdit.ReplyId))
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

        public async Task<ReplyDetail> Delete(int? id)
        {
            var reply = await _context.Replies.FirstOrDefaultAsync(m => m.ReplyId == id);
            var replyDetail = new ReplyDetail
            {
                Content = reply.Content,
                ReplyId = reply.ReplyId,
                CreatedUtc = reply.CreatedUtc,
                ModifiedUtc = reply.ModifiedUtc
            };
            return replyDetail;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            _context.Replies.Remove(reply);
            return await _context.SaveChangesAsync() == 1;
        }

        private bool ReplyExists(int id)
        {
            return _context.Replies.Any(e => e.ReplyId == id);
        }
    }
}