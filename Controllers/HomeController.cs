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
                // �p�GTempData������ơA�h�qTempData��Ū��
                var jsonData = TempData["TableData"] as string;
                tableData = System.Text.Json.JsonSerializer.Deserialize<List<IncomeTableViewModel>>(jsonData);

            }
            else
            {
                // �p�GTempData���S����ơA�h�Ыؤ@�ӷs���C��
                // ���������
                tableData = new List<IncomeTableViewModel>
                {
                    new IncomeTableViewModel { Id = 1, sType = "��X", sDate = Convert.ToDateTime("2025-01-01"),Amount=3000 ,Description=""},
                    new IncomeTableViewModel { Id = 2, sType = "��X", sDate = Convert.ToDateTime("2025-01-02"),Amount=1600 ,Description="" },
                    new IncomeTableViewModel { Id = 3, sType = "��X", sDate = Convert.ToDateTime("2025-01-03"),Amount=8000 ,Description="" }
                };

                //�N��ƼȦs��tempData��
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
            // �q TempData ��Ū�� JSON �äϧǦC�Ƭ�����
            var tableData = new List<IncomeTableViewModel>();
            if (TempData["TableData"] != null)
            {
                var jsonData = TempData["TableData"] as string;
                tableData = System.Text.Json.JsonSerializer.Deserialize<List<IncomeTableViewModel>>(jsonData);
            }

            // �p��s�� ID�G���ثe�C���̤j�� ID�A�M��[ 1
            int newId = tableData.Count()>0 ? tableData.Max(x => x.Id) + 1 : 1;
            income.Id = newId;

            // �N�s�����J���إ[�J�C��
            tableData.Add(income);

            // �N��s�᪺��ƧǦC�Ƭ� JSON �æs�J TempData
            TempData["TableData"] = System.Text.Json.JsonSerializer.Serialize(tableData);

            return RedirectToAction("Index");
        }

    }
}
