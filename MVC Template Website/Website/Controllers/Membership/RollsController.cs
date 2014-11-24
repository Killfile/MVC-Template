using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Template.Authentication.Model;
using Template.Authentication.Persistance;
using Template.MappingContract;
using Template.Website.ViewModels.Membership.Rolls;

namespace Template.Website.Controllers.Membership
{
    public class RollsController : Controller
    {
        private readonly IRollPersistance _rollPersistance;
        private readonly MappingBaseline _mapper;

        public RollsController(IRollPersistance rollPersistance, MappingBaseline mapper)
        {
            _rollPersistance = rollPersistance;
            _mapper = mapper;
        }

        // GET: Rolls
        public ActionResult Index()
        {
            List<Roll> allRolls = _rollPersistance.GetAll();
            var viewModel = _mapper.Map<List<Roll>, List<EditRollViewModel>>(allRolls);
            return View(viewModel);
        }

        // GET: Rolls/Details/5
        public ActionResult Details(Guid id)
        {
            var roll = _rollPersistance.Get(id);
            var viewModel = _mapper.Map<Roll, EditRollViewModel>(roll);
            return View(viewModel);
        }

        // GET: Rolls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rolls/Create
        [HttpPost]
        public ActionResult Create(CreateRollViewModel collection)
        {
            try
            {
                var domainModel = _mapper.Map<CreateRollViewModel, Roll>(collection);
                domainModel.RollID = Guid.NewGuid();
                _rollPersistance.Insert(domainModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rolls/Edit/5
        public ActionResult Edit(Guid id)
        {
            var roll = _rollPersistance.Get(id);
            var viewModel = _mapper.Map<Roll, EditRollViewModel>(roll);
            return View(viewModel);
        }

        // POST: Rolls/Edit/5
        [HttpPost]
        public ActionResult Edit(EditRollViewModel collection)
        {
            try
            {
                var domainModel = _mapper.Map<EditRollViewModel, Roll>(collection);
                _rollPersistance.Update(domainModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rolls/Delete/5
        public ActionResult Delete(Guid id)
        {
            var domainModel = _rollPersistance.Get(id);
            var viewModel = _mapper.Map<Roll,EditRollViewModel>(domainModel);
            return View(viewModel);
        }

        // POST: Rolls/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _rollPersistance.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
