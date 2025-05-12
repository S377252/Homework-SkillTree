using System.Diagnostics;
using Homework_SkillTree.Data;
using Homework_SkillTree.Models;
using Homework_SkillTree.Models.DB;
using Homework_SkillTree.Service;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace Homework_SkillTree.Controllers
{
    public class HomeController (
        ILogger<HomeController> _logger,
        AccountDBContext dBContext,
        IAccountService accountService
        )
        : Controller
    {

        // GET: Home/Index 開啟首頁
        public async Task<IActionResult> Index(int? page)
        {
            // 取得所有帳本資料
            var accountBooks = await accountService.GetAllAccountBooks();

            // 設定每頁顯示的項目數量
            int pageSize = 10;
            int pageNumber = page ?? 1; // 如果 page 為 null，預設為第 1 頁

            // 將資料轉換為分頁格式
            var pagedList = accountBooks.ToPagedList(pageNumber, pageSize);

            // 將分頁資料傳遞到視圖
            return View(pagedList);
        }

        // GET: Home/Add 開啟新增頁面
        public IActionResult Add()
        {
            return View();
        }

        // POST: Home/AddIncome 新增資料
        [HttpPost]
        public async Task<IActionResult> AddIncome(JoinActBook income)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", income); // 有錯就回原畫面，顯示驗證錯誤
            }

            // 驗證資料
            if (string.IsNullOrEmpty(income.Description))
            {
                income.Description = "";
            }
            // DB新增資料
            await accountService.AddAccountBook(income);

            // 重新導向到 Index
            return RedirectToAction("Index");
        }


        /* 未連DB 新增及顯示列表
        public IActionResult Index()
        {
            // 從靜態類別中取得資料
            var tableData = IncomeTableData.GetTableData();

            // string>>ViewName
            // object>>Model
            // string,object>>顯示string中的View頁面，並傳入object物件，使得在View頁面中可以用宣告Model方式使用這個物件
            return View(tableData); //將資料傳到 Index【IActionResult 名稱】 頁面
            //return View("Add",tableData);//將資料傳到 Add 頁面
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


        */

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
