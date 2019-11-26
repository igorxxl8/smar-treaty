using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Core.Services.Interfaces;
using SmarTreaty.ViewModels.Trainers;

namespace SmarTreaty.Areas.admin.Controllers
{
    [RoutePrefix("trainers")]
    [Route("{action}/{id}")]
    public class TrainersController : AdminController
    {
        private readonly ITrainerService _trainerService;
        private readonly ITrainerGroupService _trainerGroupService;
        private readonly IUserService _userService;

        public TrainersController(ITrainerGroupService trainerGroupService, ITrainerService trainerService,
            IUserService userService)
        {
            _trainerGroupService = trainerGroupService;
            _trainerService = trainerService;
            _userService = userService;
        }

        [Route("")]
        public ActionResult Index(string search)
        {
            IEnumerable<Trainer> tr;
            if (string.IsNullOrEmpty(search))
            {
                tr = _trainerService.GetTrainers(properties: "TrainerGroup,User");
            }
            else
            {
                tr = _trainerService.GetTrainers(t => t.User.LastName.Contains(search)
                                                   || t.User.FirstName.Contains(search), properties: "TrainerGroup,User");
            }

            var trainers = tr
                .Select(t => new IndexTrainerViewModel(t));
            return View(trainers);
        }

        [Route("create")]
        public ActionResult Create()
        {
            ViewBag.TrainerGroupsList = new SelectList(_trainerGroupService.GetTrainerGroups(), "Id", "Name");
            var listItems = _userService.GetUsers(u => u.Trainer == null)
                .Select(s => new {ID = s.Id, FullName = s.FirstName + " " + s.LastName});
            ViewBag.UsersList = new SelectList(listItems, "Id", "FullName");
            var vm = new CreateTrainerViewModel();
            return View(vm);
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTrainerViewModel trainerVm)
        {
            if (ModelState.IsValid)
            {
                _trainerService.AddTrainer(trainerVm.GetTrainer());
                _trainerService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.TrainerGroupsList = new SelectList(_trainerGroupService.GetTrainerGroups(), "Id", "Name");
            var listItems = _userService.GetUsers(predicate: u => u.Trainer == null)
                .Select(s => new {ID = s.Id, FullName = s.FirstName + " " + s.LastName});
            ViewBag.UsersList = new SelectList(listItems, "Id", "FullName");
            return View(trainerVm);
        }

        [Route("edit/{id}")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var trainer = _trainerService.GetTrainer(id.Value);
            if (trainer == null)
            {
                return HttpNotFound();
            }

            ViewBag.TrainerGroupsList = new SelectList(_trainerGroupService.GetTrainerGroups(), "Id", "Name");
            ViewBag.Id = new SelectList(_userService.GetUsers(), "Id", "Email", trainer.Id);
            var vm = new EditTrainerViewModel(trainer);
            return View(vm);
        }

        [Route("edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTrainerViewModel editTrainerVm)
        {
            if (ModelState.IsValid)
            {
                _trainerService.UpdateTrainer(editTrainerVm.GetTrainer());
                _trainerService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.TrainerGroupsList = new SelectList(_trainerGroupService.GetTrainerGroups(), "Id", "Name");
            ViewBag.Id = new SelectList(_userService.GetUsers(), "Id", "Email");
            return View(editTrainerVm);
        }

        [Route("delete/{id}")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var trainer = _trainerService.GetTrainer(id.Value);
            if (trainer == null)
            {
                return HttpNotFound();
            }

            var vm = new DeleteTrainerViewModel(trainer);
            return View(vm);
        }

        [Route("delete/{id}")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _trainerService.DeleteTrainer(id);
            _trainerService.Save();
            return RedirectToAction("Index");
        }
    }
}