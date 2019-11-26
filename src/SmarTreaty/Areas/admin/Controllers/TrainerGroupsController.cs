using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmarTreaty.Core.Services.Interfaces;
using SmarTreaty.ViewModels.TrainerGroups;

namespace SmarTreaty.Areas.admin.Controllers
{
    [RoutePrefix("trainergroups")]
    [Route("{action}/{id}")]
    public class TrainerGroupsController : AdminController
    {
        private readonly ITrainerGroupService _trainerGroupService;

        public TrainerGroupsController(ITrainerGroupService trainerGroupService)
        {
            _trainerGroupService = trainerGroupService;
        }

        [Route("")]
        public ActionResult Index()
        {
            var trainerGroups =
                _trainerGroupService.GetTrainerGroups().Select(tg => new IndexTrainerGroupViewModel(tg));
            return View(trainerGroups);
        }

        [Route("create")]
        public ActionResult Create()
        {
            var vm = new CreateTrainerGroupViewModel();
            return View(vm);
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTrainerGroupViewModel trainerGroupVm)
        {
            if (!ModelState.IsValid) return View(trainerGroupVm);

            _trainerGroupService.AddTrainerGroup(trainerGroupVm.GetTrainerGroup());
            _trainerGroupService.Save();
            return RedirectToAction("Index");
        }

        [Route("edit/{id}")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var trainerGroup = _trainerGroupService.GetTrainerGroup(id.Value);
            if (trainerGroup == null)
            {
                return HttpNotFound();
            }

            var vm = new EditTrainerGroupViewModel(trainerGroup);
            return View(vm);
        }

        [Route("edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTrainerGroupViewModel trainerGroupVm)
        {
            if (!ModelState.IsValid) return View(trainerGroupVm);

            _trainerGroupService.UpdateTrainerGroup(trainerGroupVm.GetTrainerGroup());
            _trainerGroupService.Save();
            return RedirectToAction("Index");
        }

        [Route("delete/{id}")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var trainerGroup = _trainerGroupService.GetTrainerGroup(id.Value);
            if (trainerGroup == null)
            {
                return HttpNotFound();
            }

            var vm = new DeleteTrainerGroupViewModel(trainerGroup);
            return View(vm);
        }

        [Route("delete/{id}")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _trainerGroupService.DeleteTrainerGroup(id);
            _trainerGroupService.Save();
            return RedirectToAction("Index");
        }
    }
}