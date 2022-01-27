using CrowdGod.Model.QuestionModel;

namespace CrowdGod.Service
{
    public interface IQuestionService
    {
        Task<QuestionSortViewModel> GetAllAsync(string searchString);

        Task<List<QuestionListItem>> GetAllAsync();

        Task<QuestionDetail> Details(int? id);

        Task<bool> Create(QuestionCreate Question);

        Task<QuestionEdit> Edit(int? id);

        Task<bool> Edit(int id, QuestionEdit Question);

        Task<QuestionDetail> Delete(int? id);

        Task<bool> DeleteConfirmed(int id);
    }
}
