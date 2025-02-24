using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Instructors
                .Include(i => i.Course)
                .Include(i => i.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.Course)
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Image,Salary,Address,CourseId,DepartmentId")] Instructor instructor)
        {
            // Manual validation
            var errors = new List<string>();

            if (string.IsNullOrEmpty(instructor.Name))
            {
                errors.Add("Name is required.");
            }
            if (string.IsNullOrEmpty(instructor.Image))
            {
                errors.Add("Image URL is required.");
            }
            if (instructor.Salary <= 0)
            {
                errors.Add("Salary must be greater than zero.");
            }
            if (string.IsNullOrEmpty(instructor.Address))
            {
                errors.Add("Address is required.");
            }
            if (instructor.CourseId <= 0)
            {
                errors.Add("A valid course must be selected.");
            }
            if (instructor.DepartmentId <= 0)
            {
                errors.Add("A valid department must be selected.");
            }

            if (errors.Any())
            {
                // Re-populate dropdown lists
                ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", instructor.CourseId);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DepartmentId);

                // Add errors to ViewData to display in the view
                ViewData["Errors"] = errors;
                return View(instructor);
            }

            try
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                // Log the exception and show a generic error message
                ViewData["Errors"] = new List<string> { "Unable to save changes. Please try again." };
                ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", instructor.CourseId);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DepartmentId);
                return View(instructor);
            }
        }



        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", instructor.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,Salary,Address,CourseId,DepartmentId")] Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.Id))
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

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", instructor.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.Course)
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.Id == id);
        }
    }
}
