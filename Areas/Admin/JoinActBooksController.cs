using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework_SkillTree.Data;
using Homework_SkillTree.Models.DB;

namespace Homework_SkillTree.Areas.Admin
{
    [Area("Admin")]
    public class JoinActBooksController : Controller
    {
        private readonly AccountDBContext _context;

        public JoinActBooksController(AccountDBContext context)
        {
            _context = context;
        }

        // GET: Admin/JoinActBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.JoinActBooks.ToListAsync());
        }

        // GET: Admin/JoinActBooks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joinActBook = await _context.JoinActBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joinActBook == null)
            {
                return NotFound();
            }

            return View(joinActBook);
        }

        // GET: Admin/JoinActBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/JoinActBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Categoryyy,sDate,Amount,Description")] JoinActBook joinActBook)
        {
            if (ModelState.IsValid)
            {
                joinActBook.Id = Guid.NewGuid();
                _context.Add(joinActBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var joinActBook = await _context.JoinActBooks.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Categoryyy,sDate,Amount,Description")] JoinActBook joinActBook)
        {
            if (id != joinActBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joinActBook);
                    await _context.SaveChangesAsync();
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

            var joinActBook = await _context.JoinActBooks
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
            var joinActBook = await _context.JoinActBooks.FindAsync(id);
            if (joinActBook != null)
            {
                _context.JoinActBooks.Remove(joinActBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JoinActBookExists(Guid id)
        {
            return _context.JoinActBooks.Any(e => e.Id == id);
        }
    }
}
