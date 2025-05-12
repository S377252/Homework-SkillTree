using Homework_SkillTree.Data;
using Homework_SkillTree.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace Homework_SkillTree.Service
{
    public class AccountService(AccountDBContext dBContext) : IAccountService
    { 
        //取得所有帳本資料
        public async Task<List<JoinActBook>> GetAllAccountBooks()
        {
            var accountBooks = await dBContext.JoinActBooks
                .AsNoTracking()
                .Select(x => new JoinActBook
                {
                    Id = x.Id,
                    sType = x.sType,
                    sDate = x.sDate,
                    Amount = x.Amount,
                    Description = x.Description
                })
                .ToListAsync();
            return accountBooks;
        }

        //新增帳本資料
        public async Task AddAccountBook(JoinActBook accountBook)
        {
            //將資料加入資料庫
            dBContext.JoinActBooks.Add(accountBook);
            await dBContext.SaveChangesAsync();
        }

    }
}
