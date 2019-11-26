using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SmarTreaty.Core.Services.Interfaces;
using SmarTreaty.ViewModels.TrainerGroups;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("trainers")]
    public class TrainerGroupsController : DefaultController
    {
        private readonly ITrainerGroupService _trainerGroupService;

        public TrainerGroupsController(ITrainerGroupService trainerGroupService)
        {
            _trainerGroupService = trainerGroupService;
        }

        [Route("")]
        public ActionResult Index(string search)
        {
            IEnumerable<IndexTrainerGroupViewModel> trainerGroups;
            if (string.IsNullOrEmpty(search))
            {
                trainerGroups = _trainerGroupService
                    .GetTrainerGroups()
                    .Select(tg => new IndexTrainerGroupViewModel(tg));
            }
            else
            {
                search = search.ToLower();
                trainerGroups = _trainerGroupService
                    .GetTrainerGroups(tg => tg.Trainers.Any(t => (t.User.FirstName + " " + t.User.LastName).ToLower().Contains(search)))
                    .Select(tg =>
                    {
                        var trainers = tg.Trainers.Where(t => (t.User.FirstName + " " + t.User.LastName).ToLower().Contains(search));
                        return new IndexTrainerGroupViewModel(tg, trainers.ToList());
                    });
            }
            return View(trainerGroups);
        }
    }
}