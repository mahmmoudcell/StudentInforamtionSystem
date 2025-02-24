using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class crsResultsController : Controller
    {
        private readonly Data.ApplicationDbContext _context;

        public crsResultsController(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: crsResults
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.crsResult.Include(c => c.Course).Include(c => c.Trainee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: crsResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crsResult = await _context.crsResult
                .Include(c => c.Course)
                .Include(c => c.Trainee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crsResult == null)
            {
                return NotFound();
            }

            return View(crsResult);
        }

        // GET: crsResults/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["TraineeId"] = new SelectList(_context.Trainees, "Id", "Id");
            return View();
        }

        // POST: crsResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Degree,CourseId,TraineeId")] crsResult crsResult)
        {
            _context.Add(crsResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: crsResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crsResult = await _context.crsResult.FindAsync(id);
            if (crsResult == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", crsResult.CourseId);
            ViewData["TraineeId"] = new SelectList(_context.Trainees, "Id", "Id", crsResult.TraineeId);
            return View(crsResult);
        }

        // POST: crsResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Degree,CourseId,TraineeId")] crsResult crsResult)
        {
            if (id != crsResult.Id)
            {
                return NotFound();
            }


            try
            {
                _context.Update(crsResult);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!crsResultExists(crsResult.Id))
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

        // GET: crsResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crsResult = await _context.crsResult
                .Include(c => c.Course)
                .Include(c => c.Trainee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crsResult == null)
            {
                return NotFound();
            }

            return View(crsResult);
        }

        // POST: crsResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crsResult = await _context.crsResult.FindAsync(id);
            if (crsResult != null)
            {
                _context.crsResult.Remove(crsResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool crsResultExists(int id)
        {
            return _context.crsResult.Any(e => e.Id == id);
        }
    }
}