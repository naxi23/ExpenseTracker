using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Domain.DTOs;

namespace ExpenseTracker.MVC.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpenseDBContext _context;

        public ExpensesController(ExpenseDBContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var expenseDBContext = _context.Expenses.Include(e => e.SubCategory);
            return View(await expenseDBContext.ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.SubCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        public IActionResult Create()
        {
            ViewData["SubCategoryID"] = new SelectList(_context.SubCategories, "Id", "SubCategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseModel expenseModel)
        {
            Expense expense = new();
            if (ModelState.IsValid)
            {
                expense.Name = expenseModel.Name;
                expense.Description = expenseModel.Description;
                expense.Amount = expenseModel.Amount;
                expense.SubCategoryID = expenseModel.SubCategoryID;
                expense.CreatedDate = expenseModel.CreatedDate;
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubCategoryID"] = new SelectList(_context.SubCategories, "Id", "SubCategoryName", expense.SubCategoryID);
            return View(expense);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["SubCategoryID"] = new SelectList(_context.SubCategories, "Id", "SubCategoryName", expense.SubCategoryID);
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (expense != null)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
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
            ViewData["SubCategoryID"] = new SelectList(_context.SubCategories, "Id", "SubCategoryName", expense.SubCategoryID);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.SubCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
