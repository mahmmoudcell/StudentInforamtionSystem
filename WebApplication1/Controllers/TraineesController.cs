using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplication1.Models;

public class TraineesController : Controller
{
    private readonly WebApplication1.Data.ApplicationDbContext _context;

    public TraineesController(WebApplication1.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    private bool TraineeExists(int id)
    {
        return _context.Trainees.Any(e => e.Id == id);
    }

    // GET: Trainees
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Trainees.Include(t => t.Department);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Trainees/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trainee = await _context.Trainees
            .Include(t => t.Department)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (trainee == null)
        {
            return NotFound();
        }

        return View(trainee);
    }

    // GET: Trainees/Create
    public IActionResult Create()
    {
        PopulateDepartmentsDropDownList();
        return View();
    }

    // POST: Trainees/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Image,Address,Grade,DepartmentId")] Trainee trainee)
    {
        // Manual validation
        var errors = new List<string>();

        if (string.IsNullOrEmpty(trainee.Name))
        {
            errors.Add("Name is required.");
        }
        if (string.IsNullOrEmpty(trainee.Image))
        {
            errors.Add("Image URL or path is required.");
        }
        if (string.IsNullOrEmpty(trainee.Address))
        {
            errors.Add("Address is required.");
        }
        if (trainee.Grade <= 0)
        {
            errors.Add("Grade must be greater than zero.");
        }
        if (trainee.DepartmentId <= 0)
        {
            errors.Add("A valid department must be selected.");
        }

        if (errors.Any())
        {
            PopulateDepartmentsDropDownList(trainee.DepartmentId);
            ViewData["Errors"] = errors;
            return View(trainee);
        }

        try
        {
            _context.Add(trainee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ViewData["Errors"] = new List<string> { "Unable to save changes. Please try again." };
            PopulateDepartmentsDropDownList(trainee.DepartmentId);
            return View(trainee);
        }
    }
    // GET: Trainees/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trainee = await _context.Trainees.FindAsync(id);
        if (trainee == null)
        {
            return NotFound();
        }

        // Populate drop-down lists
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", trainee.DepartmentId);

        return View(trainee);
    }


    // POST: Trainees/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,Address,Grade,DepartmentId")] Trainee trainee)
    {
        if (id != trainee.Id)
        {
            return NotFound();
        }

        // Ensure the departmentId is valid
        var departmentExists = await _context.Departments.AnyAsync(d => d.Id == trainee.DepartmentId);
        if (!departmentExists)
        {
            ModelState.AddModelError("DepartmentId", "Selected department does not exist.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingTrainee = await _context.Trainees.FindAsync(id);
                if (existingTrainee == null)
                {
                    return NotFound();
                }

                _context.Entry(existingTrainee).CurrentValues.SetValues(trainee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeExists(trainee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // Repopulate the drop-down list in case of validation failure
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", trainee.DepartmentId);
        return View(trainee);
    }


    // GET: Trainees/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trainee = await _context.Trainees
            .Include(t => t.Department)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (trainee == null)
        {
            return NotFound();
        }

        return View(trainee);
    }

    // POST: Trainees/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var trainee = await _context.Trainees.FindAsync(id);
        if (trainee != null)
        {
            _context.Trainees.Remove(trainee);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
    {
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", selectedDepartment);
    }
}
