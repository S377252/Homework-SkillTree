using Homework_SkillTree.Models;

namespace Homework_SkillTree.Models
{
    public class IncomeTableViewModel
    {
        public int Id { get; set; }
        public string sType { get; set; }
        public DateTime sDate { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        
    }

    ////設定IncomeTableView預設值

    //tableData = new List<IncomeTableViewModel>
    //            {
    //                new IncomeTableViewModel { Id = 1, sType = "支出", sDate = Convert.ToDateTime("2025-01-01"),Amount=300 ,Description=""},
    //                new IncomeTableViewModel { Id = 2, sType = "支出", sDate = Convert.ToDateTime("2025-01-02"), Amount = 1600, Description = "" },
    //                new IncomeTableViewModel { Id = 3, sType = "支出", sDate = Convert.ToDateTime("2025-01-03"), Amount = 8000, Description = "" }
    //            };    

}
