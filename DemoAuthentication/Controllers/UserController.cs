using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using DemoAuthentication.Classes;
using DemoAuthentication.Models;
using DemoAuthentication.ServiceReference1;

namespace TTTV_HostApplication.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Roles/
        Level2ServiceClient proxy = new Level2ServiceClient();
        UserDataAccessConsolidator access = new UserDataAccessConsolidator();
        private const int defaultPageSize = 5;
        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var userList = access.getUserList().OrderBy(e => e.UserName);
            if (Request.IsAjaxRequest())
                return PartialView("_ListControl", userList.ToPagedList(currentPageIndex, defaultPageSize));
            else
                return View(userList.ToPagedList(currentPageIndex, defaultPageSize));
        }

        public ActionResult Search(int? page, string keySearch = "Name", string valSearch = "")
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            ViewData["keySearch"] = keySearch;
            ViewData["valSearch"] = valSearch;
            var userList = access.getUserList().OrderBy(e => e.UserName);
            var list = userList.Where(e => e.UserName.ToLower().Contains(valSearch.ToLower())).Select(e => e).OrderBy(e => e.UserName).ToList();
            return PartialView("_ListControl", list.ToPagedList(currentPageIndex, defaultPageSize));
        }
        //
        // GET: /Roles/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Roles/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Edit/5

        public ActionResult Edit(string id)
        {
            TTTV__Roles r = new TTTV__Roles();

            UserLoginModel login = access.getUserLoginById(id);
            UserModel user = access.getUserById(id);
            ViewData["Roles"] = new SelectList(proxy.getAllRoles(), "RoleId", "RoleName", login.roleID);
            AccountWrapper wrap = new AccountWrapper()
            {
                user = user,
                login = login
            };
            return View(wrap);
        }

        //
        // POST: /Roles/Edit/5

        [HttpPost]
        public ActionResult Edit(AccountWrapper model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name.ToString();
                proxy.updateUserInfo(model.user.UserName, model.user.Companyname, model.user.BusinessCode, model.user.Phone, model.user.Mobile, model.user.CMND, model.user.Name, model.user.Age, model.user.Address, model.user.SelectedProvinceValue, model.user.SelectedDistrictValue, model.user.SelectedDetailProvincesValue, model.login.Email, model.login.roleID);
                return RedirectToAction("Index");
            }

            ViewData["Roles"] = new SelectList(proxy.getAllRoles(), "RoleId", "RoleName");
            return View(model);
        }

        //
        // GET: /Roles/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Roles/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
