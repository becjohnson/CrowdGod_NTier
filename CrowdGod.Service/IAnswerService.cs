using CrowdGod.Model.AnswerModel;

namespace CrowdGod.Service
{
    public interface IAnswerService
    {
        Task<AnswerSortViewModel> GetAllAsync(string searchString);

        Task<List<AnswerListItem>> GetAllAsync();

        Task<AnswerDetail> Details(int? id);

        Task<bool> Create(int questionId, AnswerCreate answer);

        Task<AnswerEdit> Edit(int? id);

        Task<bool> Edit(int id, AnswerEdit answer);

        Task<AnswerDetail> Delete(int? id);

        Task<bool> DeleteConfirmed(int id);
    }
}