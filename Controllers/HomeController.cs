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
            // 從靜態類別中取得資料
            var tableData = IncomeTableData.GetTableData();
            return View(tableData);
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
            // 使用靜態類別新增資料
            IncomeTableData.AddIncome(income);

            // 重新導向到 Index
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

    }
    public static class IncomeTableData
    {
        // 靜態列表，用於存儲 IncomeTableViewModel 資料
        private static List<IncomeTableViewModel> _tableData = new List<IncomeTableViewModel>
        {
            // 初始資料
            new IncomeTableViewModel { Id = 1, sType = "支出", sDate = Convert.ToDateTime("2025-01-01"), Amount = 3000, Description = "" },
            new IncomeTableViewModel { Id = 2, sType = "支出", sDate = Convert.ToDateTime("2025-01-02"), Amount = 1600, Description = "" },
            new IncomeTableViewModel { Id = 3, sType = "支出", sDate = Convert.ToDateTime("2025-01-03"), Amount = 8000, Description = "" }
        };

        // 提供讀取資料的方法
        public static List<IncomeTableViewModel> GetTableData()
        {
            return _tableData;
        }

        // 提供新增資料的方法
        public static void AddIncome(IncomeTableViewModel income)
        {
            // 計算新的 ID
            int newId = _tableData.Any() ? _tableData.Max(x => x.Id) + 1 : 1;
            income.Id = newId;

            // 新增資料到列表
            _tableData.Add(income);
        }
    }
}
