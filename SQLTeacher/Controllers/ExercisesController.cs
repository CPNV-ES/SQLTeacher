using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLTeacher.Models;

namespace SQLTeacher.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly SQLTeacherContext _context;

        public ExercisesController(SQLTeacherContext context)
        {
            _context = context;
        }

        // Before loading any page
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.user = StartController._currentUser; //Put user in viewbag
            if (ViewBag.user.Role.Name != "teacher")
            {
                filterContext.Result = new RedirectToRouteResult('/');
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exercises.ToListAsync());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercises = await _context.Exercises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercises == null)
            {
                return NotFound();
            }

            return View(exercises);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public int Create([FromBody] Exercises exercises)
        {
           if (ModelState.IsValid)
            {
                _context.Add(exercises);
                _context.SaveChanges();
                return _context.Exercises.Max(item => item.Id);
            }
            return 0;
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercises = await _context.Exercises.FindAsync(id);
            if (exercises == null)
            {
                return NotFound();
            }
            return View(exercises);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DbScript,Title")] Exercises exercises)
        {
            if (id != exercises.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercises);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExercisesExists(exercises.Id))
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
            return View(exercises);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercises = await _context.Exercises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercises == null)
            {
                return NotFound();
            }

            return View(exercises);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercises = await _context.Exercises.FindAsync(id);
            _context.Exercises.Remove(exercises);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExercisesExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<bool> Activate(int id, [FromBody] Exercises exercises)
        {
            if (id != exercises.Id)
            {
                return false;
            }
            try
            {
                // Put all exercises to false
                foreach(Exercises exercise in await _context.Exercises.ToListAsync())
                {
                    exercise.IsActive = false;
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }

                // Get the current exersise
                Exercises oldExercises = await _context.Exercises.FirstOrDefaultAsync(m => m.Id == id);
                if (oldExercises == null)
                {
                    return false;
                }
                // Set the received value and save
                oldExercises.IsActive = exercises.IsActive;
                _context.Update(oldExercises);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
    }
}
