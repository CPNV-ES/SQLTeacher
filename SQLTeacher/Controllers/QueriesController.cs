﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLTeacher.Models;

namespace SQLTeacher.Controllers
{
    public class QueriesController : Controller
    {
        private readonly SQLTeacherContext _context;

        public QueriesController(SQLTeacherContext context)
        {
            _context = context;
        }

        // GET: Queries
        public async Task<IActionResult> Index()
        {
            var sQLTeacherContext = _context.Queries.Include(q => q.Exercise);
            return View(await sQLTeacherContext.ToListAsync());
        }

        // GET: Queries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queries = await _context.Queries
                .Include(q => q.Exercise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (queries == null)
            {
                return NotFound();
            }

            return View(queries);
        }

        // GET: Queries/Create
        public IActionResult Create()
        {
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Title");
            return View();
        }

        // POST: Queries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Statement,Formulation,Rank,ExerciseId")] Queries queries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(queries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Title", queries.ExerciseId);
            return View(queries);
        }

        // GET: Queries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queries = await _context.Queries.FindAsync(id);
            if (queries == null)
            {
                return NotFound();
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Title", queries.ExerciseId);
            return View(queries);
        }

        // POST: Queries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Statement,Formulation,Rank,ExerciseId")] Queries queries)
        {
            if (id != queries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueriesExists(queries.Id))
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
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Title", queries.ExerciseId);
            return View(queries);
        }

        // GET: Queries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queries = await _context.Queries
                .Include(q => q.Exercise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (queries == null)
            {
                return NotFound();
            }

            return View(queries);
        }

        // POST: Queries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queries = await _context.Queries.FindAsync(id);
            _context.Queries.Remove(queries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueriesExists(int id)
        {
            return _context.Queries.Any(e => e.Id == id);
        }
    }
}