using Homework_SkillTree.Models.DB;

namespace Homework_SkillTree.Service
{
    public interface IAccountService
    {
        Task<List<JoinActBook>> GetAllAccountBooks();

        Task AddAccountBook(JoinActBook accountBook);

    }
}
