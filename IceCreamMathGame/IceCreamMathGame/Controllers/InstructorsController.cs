using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IceCreamMathGame.Data;
using IceCreamMathGame.Models;

namespace IceCreamMathGame.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IceCreamContext _context;

        public InstructorsController(IceCreamContext context)
        {
            _context = context;    
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructors.ToListAsync());
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .SingleOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        [HttpGet]
        public IActionResult InstructorLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InstructorLogin(InstructorM i)
        {
            bool find = _context.Instructors.ToList().Any(m => m.UserName == i.UserName && m.Password == i.Password);
            if (find)
            {
                //ViewBag.error = "Name Already exists";
                return RedirectToAction("Contact", "Home");
            }
            else
            {
                ViewBag.error = "User Name Or Password is wrong or does not exist!";
                return View("InstructorLogin");
            }
            //return View();
        }

        // GET: Instructors/Create
        public IActionResult InstructorRegister()
        {
            InstructorM instructor = new InstructorM();
            
            return View();
        }

        [HttpGet]
        public IActionResult Preferences()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InstructorRegister([Bind("ID,LastName,FirstName,UserName,Password,Email")] InstructorM instructor)
        {
            bool find = _context.Instructors.ToList().Any(m => m.UserName == instructor.UserName);
            if (find)
            {
                ViewBag.Error = "User Name already exists, Please choose a different User Name";
                instructor.UserName = "";
                return View();
            }
            else if (ModelState.IsValid)
            {
                ViewBag.success = instructor.FirstName + " Your Account Has Been Succesfully Created! Go Back To Login";
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                ModelState.Clear();
                return View("InstructorRegister");
            }
            
            
            return View(instructor);
        }

        

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.SingleOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,UserName,Password,Email")] InstructorM instructor)
        {
            if (id != instructor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
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
                .SingleOrDefaultAsync(m => m.ID == id);
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
            var instructor = await _context.Instructors.SingleOrDefaultAsync(m => m.ID == id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
