using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web.Script.Serialization;
using DemoAuthentication.ServiceReference1;
using DemoAuthentication.Models;

namespace DemoAuthentication.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn
        Level2ServiceClient proxy = new Level2ServiceClient();
        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                MyMembership mb = new MyMembership();
                if (mb.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            ViewBag.province = new SelectList(proxy.getAllProvince(), "ProvinceID", "ProvinceName");
            ViewBag.district = new SelectList(proxy.getAllDistrict(), "DistrictID", "DistrictName");
            ViewBag.detail = new SelectList(proxy.getAllDetailProvince(), "DetailID", "DetailName");
            return View();
         
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
           // CustomException exex = new CustomException();
            
            bool chkUsername=proxy.checkUsernameExist(model.UserName);
            bool chkEmail = proxy.checkEmailExist(model.Email);
            //if (chkUsername == true)
            //{
            //    ModelState.AddModelError("UserName", "Tên Đăng Nhập Này Đã Có Người Dùng !!!");
            //    ViewBag.province = new SelectList(proxy.getAllProvince(), "ProvinceID", "ProvinceName");
            //    ViewBag.district = new SelectList(proxy.getAllDistrict(), "DistrictID", "DistrictName");
            //    ViewBag.detail = new SelectList(proxy.getAllDetailProvince(), "DetailID", "DetailName");
            //    return View();
            //}
             
            if (chkEmail == true)
            {
                ModelState.AddModelError("Email", "Email Này Đã Có Người Dùng !!!");
                ViewBag.province = new SelectList(proxy.getAllProvince(), "ProvinceID", "ProvinceName");
                ViewBag.district = new SelectList(proxy.getAllDistrict(), "DistrictID", "DistrictName");
                ViewBag.detail = new SelectList(proxy.getAllDetailProvince(), "DetailID", "DetailName");
                return View();
            }
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                try
                {
                    proxy.InsertUser(model.UserName, model.Password, model.Email, model.SelectedProvinceValue, model.SelectedDistrictValue, model.SelectedDetailProvincesValue, 0, model.question, model.answear, model.Companyname, model.BusinessCode, model.Phone, model.Mobile, model.CMND, model.Name, model.Age, model.Address, model.subAddress);
                }
                catch(FaultException<CustomException> cex)
                {
                    FaultModel fault = new FaultModel();
                    fault.Issue = cex.Message;
                    fault.Detail = cex.Reason.ToString() ;
                    return View("fault", fault);
                    
                    
                }
                createStatus = MembershipCreateStatus.Success;

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else            // If we got this far, something failed, redisplay form
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

          
            return View(model);
        }
        #region Get Value for Dropdownlist

        public string getDistrict(int id)
        {
            var district = new List<SelectListItem>();

            var list = proxy.GetDistrictNameById(id);
            foreach (var item in list)
            {
                district.Add(new SelectListItem() { Text = item.DistrictName, Value = item.DistrictID.ToString() });
            }

            return new JavaScriptSerializer().Serialize(district);
        }
        public string getDetail(int provinceID, int id)
        {
            {
                var detail = new List<SelectListItem>();

                var list = proxy.GetProvinceDetailNameById(provinceID, id);
                foreach (var item in list)
                {
                    detail.Add(new SelectListItem() { Text = item.DetailName, Value = item.DetailID.ToString() });
                }
                return new JavaScriptSerializer().Serialize(detail);
            }
        }
        #endregion
        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
