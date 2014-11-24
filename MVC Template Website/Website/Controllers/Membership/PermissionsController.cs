using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Template.Authentication.Model;
using Template.Authentication.Persistance;
using Template.MappingContract;
using Template.Website.ViewModels.Membership.Permissions;
using Template.Website.ViewModels.Membership.Rolls;
using WebGrease.Css.Extensions;

namespace Template.Website.ContPermissioners.Membership
{
    public class PermissionsController : Controller
    {
        private readonly IPermissionPersistance _permissionPersistance;
        private readonly IRollPersistance _rollPersistance;
        private readonly MappingBaseline _mapper;

        public PermissionsController(IPermissionPersistance permissionPersistance, MappingBaseline mapper, IRollPersistance rollPersistance)
        {
            _permissionPersistance = permissionPersistance;
            _mapper = mapper;
            _rollPersistance = rollPersistance;
        }

        // GET: Permissions
        public ActionResult Index()
        {
            List<Permission> allPermissions = _permissionPersistance.GetAll();
            var viewModel = _mapper.Map<List<Permission>, List<EditPermissionViewModel>>(allPermissions);
            return View(viewModel);
        }

        /// GET: Permissions/Details/5
        public ActionResult Details(Guid id)
        {
            var Permission = _permissionPersistance.Get(id);
            var viewModel = _mapper.Map<Permission, EditPermissionViewModel>(Permission);
            return View(viewModel);
        }

        // GET: Permissions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Permissions/Create
        [HttpPost]
        public ActionResult Create(CreatePermissionViewModel collection)
        {
            try
            {
                var domainModel = _mapper.Map<CreatePermissionViewModel, Permission>(collection);
                domainModel.PermissionID = Guid.NewGuid();
                _permissionPersistance.Insert(domainModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Permissions/Edit/5
        public ActionResult Edit(Guid id)
        {
            var Permission = _permissionPersistance.Get(id);
            var viewModel = _mapper.Map<Permission, EditPermissionViewModel>(Permission);
            PopulateLinkedRollData(Permission);
            return View(viewModel);
        }

        // POST: Permissions/Edit/5
        [HttpPost]
        public ActionResult Edit(EditPermissionViewModel collection, string[] selectedRolls)
        {
            try
            {
                List<Guid> guids = selectedRolls == null ? new List<Guid>() : selectedRolls.Select(s=>new Guid(s)).ToList();
                List<Roll> rolls = _rollPersistance.GetAll(guids);


                var domainModel = _mapper.Map<EditPermissionViewModel, Permission>(collection);
                domainModel.Rolls = rolls;
                _permissionPersistance.Update(domainModel);
                return RedirectToAction("Index");
            }
            catch (Exception whatBroke)
            {
                ViewBag.ErrorMessage = string.Format(whatBroke.Message);
                ViewBag.Exception = whatBroke;
                return View();
            }
        }

        // GET: Permissions/Delete/5
        public ActionResult Delete(Guid id)
        {
            var domainModel = _permissionPersistance.Get(id);
            var viewModel = _mapper.Map<Permission, EditPermissionViewModel>(domainModel);
            return View(viewModel);
        }

        // POST: Permissions/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _permissionPersistance.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void PopulateLinkedRollData(Permission permission)
        {
            List<Roll> AllRolls = _rollPersistance.GetAll();
            var AssignedRolls = new HashSet<Guid>(permission.Rolls.Select(r => r.RollID));
            var viewModel = new List<LinkedRollViewModel>();
            foreach (Roll roll in AllRolls)
            {
                viewModel.Add(new LinkedRollViewModel
                {
                    RollID = roll.RollID,
                    Label = roll.Label,
                    Assigned = AssignedRolls.Contains(roll.RollID)
                });
            }
            ViewBag.Rolls = viewModel;
        }
    }
}
