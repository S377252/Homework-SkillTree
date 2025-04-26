using System.Diagnostics;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
      
        public IActionResult Index()
        {
            var tableData = new List<IncomeTableViewModel> {  };
            if (TempData["TableData"] != null)
            {
                // 如果TempData中有資料，則從TempData中讀取
                var jsonData = TempData["TableData"] as string;
                tableData = System.Text.Json.JsonSerializer.Deserialize<List<IncomeTableViewModel>>(jsonData);

            }
            else
            {
                // 如果TempData中沒有資料，則創建一個新的列表
                // 模擬表格資料
                tableData = new List<IncomeTableViewModel>
                {
                    new IncomeTableViewModel { Id = 1, sType = "支出", sDate = Convert.ToDateTime("2025-01-01"),Amount=3000 ,Description=""},
                    new IncomeTableViewModel { Id = 2, sType = "支出", sDate = Convert.ToDateTime("2025-01-02"),Amount=1600 ,Description="" },
                    new IncomeTableViewModel { Id = 3, sType = "支出", sDate = Convert.ToDateTime("2025-01-03"),Amount=8000 ,Description="" }
                };

                //將資料暫存於tempData中
                TempData["TableData"] = System.Text.Json.JsonSerializer.Serialize(tableData); 
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult AddIncome(IncomeTableViewModel income)
        {
            // 從 TempData 中讀取 JSON 並反序列化為物件
            var tableData = new List<IncomeTableViewModel>();
            if (TempData["TableData"] != null)
            {
                var jsonData = TempData["TableData"] as string;
                tableData = System.Text.Json.JsonSerializer.Deserialize<List<IncomeTableViewModel>>(jsonData);
            }

            // 計算新的 ID：取目前列表中最大的 ID，然後加 1
            int newId = tableData.Count()>0 ? tableData.Max(x => x.Id) + 1 : 1;
            income.Id = newId;

            // 將新的收入項目加入列表
            tableData.Add(income);

            // 將更新後的資料序列化為 JSON 並存入 TempData
            TempData["TableData"] = System.Text.Json.JsonSerializer.Serialize(tableData);

            return RedirectToAction("Index");
        }

    }
}
