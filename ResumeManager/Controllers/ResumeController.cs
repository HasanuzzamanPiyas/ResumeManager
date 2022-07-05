using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeManager.Dal;
using ResumeManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResumeManager.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public ResumeController(ResumeDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            List<Applicant> applicants;
            applicants = _context.Applicants.ToList();
            return View(applicants);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Applicant applicant = new Applicant();
            applicant.Expriences.Add(new Exprience() { ExprienceId = 1 });
            ViewBag.Gender = GetGender();
            return View(applicant);
        }

        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {

            applicant.Expriences.RemoveAll(n => n.YearWorked == 0); // not null
            applicant.Expriences.RemoveAll(n => n.IsDeleted == true);

            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;

            _context.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;

            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        public IActionResult Details(int Id)
        {
            Applicant applicant = _context.Applicants
                .Include(e => e.Expriences)
                .Where(a => a.Id == Id).FirstOrDefault();

            return View(applicant);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Applicant applicant = _context.Applicants
                .Include(e => e.Expriences)
                .Where(a => a.Id == Id).FirstOrDefault();

            return View(applicant);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Detete(Applicant applicant)
        {

            _context.Attach(applicant);

            _context.Entry(applicant).State = EntityState.Deleted;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        private List<SelectListItem> GetGender()
        {
            List<SelectListItem> selGender = new List<SelectListItem>();
            var selItem = new SelectListItem() { Value = "", Text = "Select Gender" };
            selGender.Insert(0, selItem);

            selItem = new SelectListItem()
            {
                Value = "Male",
                Text = "Male"
            };
            selGender.Add(selItem);

            selItem = new SelectListItem()
            {
                Value = "Female",
                Text = "Female"
            };
            selGender.Add(selItem);
            return selGender;
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Applicant applicant = _context.Applicants
                .Include(e => e.Expriences)
                .Where(a => a.Id == Id).FirstOrDefault();
            ViewBag.Gender = GetGender();

            return View(applicant);
        }

        [HttpPost]
        public IActionResult Edit(Applicant applicant)
        {

            List<Exprience> expDetails = _context.Expriences.Where(d => d.ApplicantId == applicant.Id).ToList();
            _context.Expriences.RemoveRange(expDetails);
            _context.SaveChanges();

            applicant.Expriences.RemoveAll(n => n.YearWorked == 0);
            applicant.Expriences.RemoveAll(n => n.IsDeleted == true);




            if(applicant.ProfilePhoto != null) 
            { 
            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;
            }

            _context.Attach(applicant);
            _context.Entry(applicant).State = EntityState.Modified;
            _context.Expriences.AddRange(applicant.Expriences);

            _context.SaveChanges();
            return RedirectToAction("index");

        }


    }
}