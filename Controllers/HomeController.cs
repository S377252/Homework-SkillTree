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
            // �q�R�A���O�����o���
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
            // �ϥ��R�A���O�s�W���
            IncomeTableData.AddIncome(income);

            // ���s�ɦV�� Index
            return RedirectToAction("Index");
        }

    }
    public static class IncomeTableData
    {
        // �R�A�C��A�Ω�s�x IncomeTableViewModel ���
        private static List<IncomeTableViewModel> _tableData = new List<IncomeTableViewModel>
        {
            // ��l���
            new IncomeTableViewModel { Id = 1, sType = "��X", sDate = Convert.ToDateTime("2025-01-01"), Amount = 3000, Description = "" },
            new IncomeTableViewModel { Id = 2, sType = "��X", sDate = Convert.ToDateTime("2025-01-02"), Amount = 1600, Description = "" },
            new IncomeTableViewModel { Id = 3, sType = "��X", sDate = Convert.ToDateTime("2025-01-03"), Amount = 8000, Description = "" }
        };

        // ����Ū����ƪ���k
        public static List<IncomeTableViewModel> GetTableData()
        {
            return _tableData;
        }

        // ���ѷs�W��ƪ���k
        public static void AddIncome(IncomeTableViewModel income)
        {
            // �p��s�� ID
            int newId = _tableData.Any() ? _tableData.Max(x => x.Id) + 1 : 1;
            income.Id = newId;

            // �s�W��ƨ�C��
            _tableData.Add(income);
        }
    }
}
