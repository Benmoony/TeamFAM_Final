using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IceCreamMathGame.Data;
using IceCreamMathGame.Models;


namespace IceCreamMathGame.Controllers
{
    
    public class InstructorsController : Controller
    {
        //Tracks the Current Logged in Instructor
        const string SessionLoggedID = "_ID"; 

        private readonly IceCreamContext _context;

        public InstructorsController(IceCreamContext context)
        {
            _context = context;    
        }

        // GET: Instructors
        // TODO: make this inaccesible to instuctors
        public async Task<IActionResult> Index()
        {
            var PassID = HttpContext.Session.GetInt32(SessionLoggedID);
            if (PassID != 27)
            {
                ViewBag.error = "You are not authorized to view this information";
                return View("InstructorIndex");
            }
                return View(await _context.Instructors.ToListAsync());
        }

        // GET: Instructors/Details/5
        // Unessessary: needs to be removed
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

                //TODO: Update Return Action to go to the Student Controller
                var instructor = _context.Instructors.ToList().SingleOrDefault(m => m.UserName == i.UserName && m.Password == i.Password);
                HttpContext.Session.SetInt32(SessionLoggedID, instructor.ID);
                return RedirectToAction("InstructorIndex");
            }
            else
            {
                ViewBag.error = "User Name Or Password is wrong or does not exist!";
                return View("InstructorLogin");
            }
            //return View();
        }

        //Method to redirect the instructor controller to the student
        public IActionResult ToStudent()
        {
            if(HttpContext.Session.GetInt32(SessionLoggedID) != null)
            {
                if(HttpContext.Session.GetInt32(SessionLoggedID) != 0)
                {
                    var PassID = HttpContext.Session.GetInt32(SessionLoggedID);
                    return RedirectToAction("Create", "Students");
                }
                else
                {
                    ViewBag.error("Please Log In");
                    return View("InstructorLogin");
                }
            }
            else
            {
                ViewBag.error("Please Log In");
                return View("InstructorLogin");
            }
        }

        // GET: Instructors/Create
        public IActionResult InstructorRegister()
        {
            InstructorM instructor = new InstructorM();
            return View();
        }

        // GET: Instructors/Preferences
        //gets the instructor preferences that will give them access to edit their account
        [HttpGet]
        public IActionResult Preferences()
        {
            var PassID = HttpContext.Session.GetInt32(SessionLoggedID);
            if(HttpContext.Session.GetInt32(SessionLoggedID) != null) { 
                if ((int)PassID == 0)
                {
                    ViewBag.error = "Please Login to your Account";
                    return View("InstructorLogin");
                }
                var instructor = _context.Instructors.SingleOrDefault(m => m.ID == (int)PassID);
                return View(instructor);
            }
            else
            {
                ViewBag.error = "Please Login to your Account";
                return View("InstructorLogin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Preferences([Bind("FirstName,LastName,UserName,Password,Email")] InstructorM instructor)
        {
            /*var PassID = (int)HttpContext.Session.GetInt32(SessionLoggedID);
            if (PassID != instructor.ID)
            {
                return NotFound();
            }*/
            var PassID = HttpContext.Session.GetInt32(SessionLoggedID);
            instructor.ID = (int)PassID;
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    _context.SaveChanges();
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
                ViewBag.Success = "Preferences Updated";
                return View("InstructorIndex");
            }
            ViewBag.Error = "Model State Invalid";
            return View("InstructorIndex");
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
                ViewBag.success = instructor.FirstName + " Your Account Has Been Succesfully Created!";
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                ModelState.Clear();
                return View("InstructorLogin");
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        //TODO: Make this only accessible by Current logged in Instructor and only able to edit from current Instructors ID
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
        //  TODO: Make this only accessible by Current logged in Instructor and only able to edit from current Instructors ID
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
        //TODO: Remove this Functionality from the website. 
        /*public async Task<IActionResult> Delete(int? id)
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
        }*/

        

        // POST: Instructors/Delete/5
        //TODO: Remove this Functionality from the website. 
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            id = (int)HttpContext.Session.GetInt32(SessionLoggedID);
            var instructor = await _context.Instructors.SingleOrDefaultAsync(m => m.ID == id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }*/

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }

        [HttpGet, ActionName("InstructorIndex")]
        public ActionResult InstructorIndex()
        {
            if(HttpContext.Session.GetInt32(SessionLoggedID) != null)
            { 
                if(HttpContext.Session.GetInt32(SessionLoggedID) != 0)
                {
                    var PassID = (int)HttpContext.Session.GetInt32(SessionLoggedID);
                    var instructor = _context.Instructors.ToList().SingleOrDefault(m => m.ID == PassID);
                    ViewBag.success = "Welcome " + instructor.FirstName + " " + instructor.LastName + "!";
                    return View();
                }
                else
                {
                    ViewBag.error = "Please Login to your Account";
                    return View("InstructorLogin");
                }
            }
            else
            {
                ViewBag.error = "Please Login to your Account";
                return View("InstructorLogin");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32(SessionLoggedID, 0);
            ViewBag.success = "You have been logged off";
            return View("InstructorLogin");
        }
    }
}
