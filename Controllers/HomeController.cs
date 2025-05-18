using System.Diagnostics;
using Homework_SkillTree.Data;
using Homework_SkillTree.Models;
using Homework_SkillTree.Models.DB;
using Homework_SkillTree.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: Home/Index �}�ҭ���
        public async Task<IActionResult> Index(int? page)
        {
            // ���o�Ҧ��b�����
            var accountBooks = await accountService.GetAllAccountBooks();

            // �]�w�C����ܪ����ؼƶq
            int pageSize = 10;
            int pageNumber = page ?? 1; // �p�G page �� null�A�w�]���� 1 ��

            // �N����ഫ�������榡
            var pagedList = accountBooks.ToPagedList(pageNumber, pageSize);

            // �N������ƶǻ������
            return View(pagedList);
        }

        // GET: Home/Add �}�ҷs�W����
        public IActionResult Add()
        {
            return View();
        }

        // POST: Home/AddIncome �s�W���
        [HttpPost]
        public async Task<IActionResult> AddIncome(JoinActBook income)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", income); // �����N�^��e���A������ҿ��~
            }

            // ���Ҹ��
            if (string.IsNullOrEmpty(income.Description))
            {
                income.Description = "";
            }
            // DB�s�W���
            await accountService.AddAccountBook(income);

            // ���s�ɦV�� Index
            return RedirectToAction("Index");
        }

        // GET: Admin/JoinActBooks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joinActBook = await dBContext.JoinActBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joinActBook == null)
            {
                return NotFound();
            }

            return View(joinActBook);
        }

        // GET: Admin/JoinActBooks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joinActBook = await dBContext.JoinActBooks.FindAsync(id);
            if (joinActBook == null)
            {
                return NotFound();
            }
            return View(joinActBook);
        }
        // POST: Admin/JoinActBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,sType,sDate,Amount,Description")] JoinActBook joinActBook)
        {
            if (id != joinActBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dBContext.Update(joinActBook);
                    await dBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JoinActBookExists(joinActBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(joinActBook);
        }


        // GET: Admin/JoinActBooks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joinActBook = await dBContext.JoinActBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joinActBook == null)
            {
                return NotFound();
            }

            return View(joinActBook);
        }

        // POST: Admin/JoinActBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var joinActBook = await dBContext.JoinActBooks.FindAsync(id);
            if (joinActBook != null)
            {
                dBContext.JoinActBooks.Remove(joinActBook);
            }

            await dBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool JoinActBookExists(Guid id)
        {
            return dBContext.JoinActBooks.Any(e => e.Id == id);
        }

        /* ���sDB �s�W����ܦC��
        public IActionResult Index()
        {
            // �q�R�A���O�����o���
            var tableData = IncomeTableData.GetTableData();

            // string>>ViewName
            // object>>Model
            // string,object>>���string����View�����A�öǤJobject����A�ϱo�bView�������i�H�ΫŧiModel�覡�ϥγo�Ӫ���
            return View(tableData); //�N��ƶǨ� Index�iIActionResult �W�١j ����
            //return View("Add",tableData);//�N��ƶǨ� Add ����
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


        */

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
