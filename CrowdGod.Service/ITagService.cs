using CrowdGod.Model.QuestionModel;

namespace CrowdGod.Service
{
    public interface ITagService
    {
        Task<QuestionSortViewModel> GetAllAsync(string searchString);
    }
}
