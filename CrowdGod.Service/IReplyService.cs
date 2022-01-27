using CrowdGod.Model.ReplyModel;

namespace CrowdGod.Service
{
    public interface IReplyService
    {
        Task<ReplySortViewModel> GetAllAsync(string searchString);

        Task<ReplySortViewModel> GetAllAsync();

        Task<ReplyDetail> Details(int? id);

        Task<bool> Create(int answerId, ReplyCreate answer);

        Task<ReplyEdit> Edit(int? id);

        Task<bool> Edit(int id, ReplyEdit answer);

        Task<ReplyDetail> Delete(int? id);

        Task<bool> DeleteConfirmed(int id);
    }
}