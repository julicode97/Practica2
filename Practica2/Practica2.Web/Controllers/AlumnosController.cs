﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practica2.Web.Data;
using Practica2.Web.Models;

namespace Practica2.Web.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlumnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alumnos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alumnos.Include(c => c.Municipio).Include(c => c.Barrio).ToListAsync());
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.Include(c => c.Municipio).Include(c => c.Barrio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Municipios = await _context.Municipios.ToListAsync();
            ViewBag.Barrios = await _context.Barrios.ToListAsync();
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Alumnos.Add(alumno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                 {
                            if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                            {
                                ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre.");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty,
                               dbUpdateException.InnerException.Message);
                            }
                        }
                        catch (Exception exception)
                        {
                            ModelState.AddModelError(string.Empty, exception.Message);
                        }
                }
            ViewBag.Municipios = await _context.Municipios.ToListAsync();
            ViewBag.Barrios = await _context.Barrios.ToListAsync();
            return View(alumno);
        }

        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            ViewBag.Municipios = await _context.Municipios.ToListAsync();
            ViewBag.Barrios = await _context.Barrios.ToListAsync();
            return View(alumno);
        }

     

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Municipios = await _context.Municipios.ToListAsync();
            ViewBag.Barrios = await _context.Barrios.ToListAsync();
            return View(alumno);
        }



        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

                Alumno alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
                if (alumno == null)
                {
                    return NotFound();
                }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }
    }
}
