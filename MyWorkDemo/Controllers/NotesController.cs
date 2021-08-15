using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWorkDemo.Data;
using MyWorkDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkDemo.Controllers
{
    public class NotesController : Controller
    {
        private MyWorkDbContext context;
        public NotesController(MyWorkDbContext db)
        {
            this.context = db;
        }
        // GET: NotesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NotesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }




        public ActionResult AddNote(long Id)
        {
            NotesDto model = new NotesDto();
         
            model = new NotesDto();
            model.ActivityId = Id;

            return View(model);
        }




        [HttpPost]
        public ActionResult AddNote(NotesDto model)
        {
            Note note = new Note();
            note.Descrizione = model.Descrizione;
            note.NoteEntryDate = DateTime.Now;
            note.ActivityId = model.id;
            context.Note.Add(note);
            context.SaveChanges();

            return View(model);
        }































        //// GET: NotesController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: NotesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: NotesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
