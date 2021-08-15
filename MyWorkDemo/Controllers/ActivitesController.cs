using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWorkDemo.Data;
using MyWorkDemo.Enum;
using MyWorkDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkDemo.Controllers
{
    [Authorize]
    public class ActivitesController : Controller
    {
        private MyWorkDbContext context;
        public ActivitesController(MyWorkDbContext db)
        {
            this.context = db;
        }
        // GET: ActivitesController
        public ActionResult Index(long? id)
         {
            List<Activity> activityList = null;

            if (id == 0)
            {
                activityList = context.Activity.Where(c => c.State.Equals(Enum.StateEnum.Backlog)).ToList();
            }
            else if (id == 1)
            {
                activityList = context.Activity.Where(c => c.State.Equals(Enum.StateEnum.InProgress)).ToList();
            }
            else if(id == 2)
            {
                activityList = context.Activity.Where(c => c.State.Equals(Enum.StateEnum.Completed)).ToList();
            }
            else
            {
                activityList = context.Activity.Where(c => c.State.Equals(Enum.StateEnum.InProgress)).ToList();
            }
            
           
            return View(activityList);
        }

        // GET: ActivitesController/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ActivityDto model = new ActivityDto();
            List<long> userIds = new List<long>();
            //Get activity 
            var activity = context.Activity.Include("ActivityUsers").FirstOrDefault(x => x.Id == id.Value);
            //Get activity Users and add each userId into UsersIds list
            activity.ActivityUsers.ToList().ForEach(result => userIds.Add(result.UserID));

             activity = context.Activity.Include("Notes").FirstOrDefault(x => x.Id == id.Value);
             activity.Notes.ToList().ForEach(result => userIds.Add(result.ActivityId));

            //bind model 
            model.drpUser = context.User.Select(x => new SelectListItem { Text = x.Username, Value = x.Id.ToString() }).ToList();
            model.Id = activity.Id;
            model.Titolo = activity.Titolo;
            model.Descrizione = activity.Descrizione;
            model.State = activity.State;
            model.Ore = activity.Ore;
            model.ActivityDate= activity.ActivityDate;
            model.UsersIds = userIds.ToArray();
            model.Notes = activity.Notes.OrderByDescending(x=> x.NoteEntryDate).ToList();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }




        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Activity model = new Activity();
            List<long> userIds = new List<long>();
            //Get activity 
            var activity = context.Activity.Include("ActivityUsers").FirstOrDefault(x => x.Id == id.Value);
            //Get activity Users and add each userId into UsersIds list
            activity.ActivityUsers.ToList().ForEach(result => userIds.Add(result.UserID));
            //bind model 
           
            model.Id = activity.Id;
            model.Titolo = activity.Titolo;
            model.Descrizione = activity.Descrizione;
            model.State = activity.State;
            model.Ore = activity.Ore;
            model.ActivityDate = activity.ActivityDate;
           

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long? id)
        {
           
            Activity act = context.Activity.Find(id);
            context.Activity.Remove(act);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: ActivitesController/Create
        public ActionResult Create()
        {
            return View();
        }

       


        public ActionResult AddActivity(long? Id)
        {
            ActivityDto model = new ActivityDto();
            List<long> userIds = new List<long>();
            if (Id.HasValue)
            {
                //Get activity 
                var activity = context.Activity.Include("ActivityUsers").FirstOrDefault(x => x.Id == Id.Value);
                //Get activity Users and add each userId into UsersIds list
                activity.ActivityUsers.ToList().ForEach(result => userIds.Add(result.UserID));

                //bind model 
                model.drpUser = context.User.Select(x => new SelectListItem { Text = x.Username, Value = x.Id.ToString() }).ToList();
                model.Id = activity.Id;
                model.Titolo = activity.Titolo;
                model.Descrizione = activity.Descrizione;
                model.State = activity.State;
                model.Ore = activity.Ore;
                model.UsersIds = userIds.ToArray();
            }
            else
            {
                model = new ActivityDto();
                model.drpUser = context.User.Select(x => new SelectListItem { Text = x.Username, Value = x.Id.ToString()}).ToList();
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult AddActivity(ActivityDto model)
        {
            Activity activity = new Activity();
            List<ActivityUser> activityUsers = new List<ActivityUser>();
            
            if (model.Id > 0)
            {
                //first find teacher subjects list and then remove all from db 
                activity = context.Activity.Include("ActivityUsers").FirstOrDefault(x => x.Id == model.Id);
                activity.ActivityUsers.ToList().ForEach(result => activityUsers.Add(result));
                context.ActivityUser.RemoveRange(activityUsers);
                //context.SaveChanges();

                //Now update teacher details
                activity.Titolo = model.Titolo;
                activity.Descrizione = model.Descrizione;
                activity.ActivityDate = DateTime.Now;
                activity.Ore  = model.Ore ;
                if (model.UsersIds.Length > 0)
                {
                    activityUsers = new List<ActivityUser>();

                    foreach (var subjectid in model.UsersIds)
                    {
                        activityUsers.Add(new ActivityUser { UserID = subjectid, ActivityID = model.Id });
                    }
                    activity.ActivityUsers = activityUsers;
                }

                if (
                    activity.State.Equals(StateEnum.Backlog) && model.State.Equals(StateEnum.Completed) ||
                    activity.State.Equals(StateEnum.Completed) && model.State.Equals(StateEnum.Backlog) ||
                    activity.State.Equals(StateEnum.InProgress) && model.State.Equals(StateEnum.Backlog)
                    )
                {
                    ViewData["message"] = "The state of the activity is not valid";
                    model.drpUser = context.User.Select(x => new SelectListItem { Text = x.Username, Value = x.Id.ToString() }).ToList();
                    return View(model);
                }


                activity.State = model.State;



                context.SaveChanges();

            }
            else
            {
                activity.Titolo = model.Titolo;
                activity.Descrizione = model.Descrizione;
                activity.State = StateEnum.Backlog;
                activity.ActivityDate = DateTime.Now;
                activity.Ore = model.Ore;
                if (model.UsersIds.Length > 0)
                {
                    foreach (var userId in model.UsersIds)
                    {
                        activityUsers.Add(new ActivityUser { UserID = userId, ActivityID = model.Id });
                    }
                    activity.ActivityUsers = activityUsers;
                }
                context.Activity.Add(activity);
                context.SaveChanges();
            }
            return RedirectToAction("index");
        }


        // GET: ActivitesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActivitesController/Edit/5
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

      
    }
}
