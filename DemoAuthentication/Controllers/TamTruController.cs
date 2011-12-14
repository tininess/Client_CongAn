using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using System.ServiceModel;
using DemoAuthentication.ServiceReference1;
using DemoAuthentication.Models;
using DemoAuthentication.Classes;
using System.Web.Helpers;

namespace DemoAuthentication.Controllers
{
    public class TamTruController : Controller
    {
        //
        // GET: /TamTru/
        Level2ServiceClient proxy = new Level2ServiceClient();
        TamTruAccessConsolidator access = new TamTruAccessConsolidator();

        private const int defaultPageSize = 3;
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int ?page)
        {
            ViewBag.province = new SelectList(proxy.getAllProvince(), "ProvinceID", "ProvinceName");
            ViewBag.district = new SelectList(proxy.getAllDistrict(), "DistrictID", "DistrictName");
            ViewBag.detail = new SelectList(proxy.getAllDetailProvince(), "DetailID", "DetailName");
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IList<TamTruModel> list = access.getAllTTTV().Where(e => e.loai == 3).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListControl", list.ToPagedList(currentPageIndex, defaultPageSize));
            }
            else
            {
                return View(list.ToPagedList(currentPageIndex, defaultPageSize));
            }
        }


        // SEARCH 
           [Authorize(Roles = "Admin")]
        public ActionResult Search(int? page, string keySearch = "code", string valSearch = "", string keyFilter = "3", bool chkDistrict = false, bool chkDetail = false, int SelectedProvinceValue = 0, int SelectedDistrictValue = 0, int SelectedDetailProvincesValue = 0)
        {
            ViewData["keySearch"] = keySearch;
            ViewData["valSearch"] = valSearch;
            ViewData["keyFilter"] = keyFilter;
            ViewData["tinh"] = SelectedProvinceValue;
            ViewData["quan"] = SelectedDistrictValue;
            ViewData["phuong"] = SelectedDetailProvincesValue;
            ViewData["chkDistrict"] = chkDistrict;
            ViewData["chkDetail"] = chkDetail;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            int filter = 3;
            switch (keyFilter)
            {
                case "1":
                    filter = 1;
                    break;
                case "2":
                    filter = 2;
                    break;
                case "3":
                    filter = 3;
                    break;
                case "0":
                    filter = 0;
                    break;
                case "4":
                    filter = 4;
                    break;
            }
            var list = access.getAllTTTV();
            if (keySearch == "code")
            {
                {
                    if (filter != 0)
                    {

                        if (filter == 4)                    // Nếu chọn đang duyệt
                        {
                            if (SelectedProvinceValue != 0)// Neu Chon Tinh Khac 0
                            {
                                if (chkDistrict == true)    // Chọn Cấp Quận
                                {

                                    if (chkDetail == true) // Chọn cấp phường
                                    {
                                        list = list.Where(e => e.TT_GiayTo.ToLower().Contains(valSearch.ToLower()) && e.SelectedStatusValue == 3 && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedDistrictValue == SelectedDistrictValue && e.SelectedDetailProvincesValue == SelectedDetailProvincesValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                    }
                                    else           // Else phường -> Chọn Quận
                                    {
                                        list = list.Where(e => e.TT_GiayTo.ToLower().Contains(valSearch.ToLower()) && e.SelectedStatusValue == 3 && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedDistrictValue == SelectedDistrictValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                    }
                                }
                                else    // else Quận Chọn tỉnh
                                {
                                    list = list.Where(e => e.TT_GiayTo.ToLower().Contains(valSearch.ToLower()) && e.SelectedStatusValue == 3 && e.SelectedProvinceValue == SelectedProvinceValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                }

                            }

                        }

                        else   // Nếu không xài đang duyệt thì
                        {
                            if (SelectedProvinceValue != 0)// Neu Chon Tinh Khac 0
                            {
                                if (chkDistrict == true)    // Chọn Cấp Quận
                                {

                                    if (chkDetail == true) // Chọn cấp phường
                                    {
                                        list = list.Where(e => e.TT_GiayTo.ToLower().Contains(valSearch.ToLower()) && e.loai == filter && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedDistrictValue == SelectedDistrictValue && e.SelectedDetailProvincesValue == SelectedDetailProvincesValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                    }
                                    else           // Else phường -> Chọn Quận
                                    {
                                        list = list.Where(e => e.TT_GiayTo.ToLower().Contains(valSearch.ToLower()) && e.loai == filter && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedDistrictValue == SelectedDistrictValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                    }
                                }
                                else    // else Quận Chọn tỉnh
                                {
                                    list = list.Where(e => e.TT_GiayTo.ToLower().Contains(valSearch.ToLower()) && e.loai == filter && e.SelectedProvinceValue == SelectedProvinceValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                    // list = list.Where(e => e.TT_FullName.ToLower().Contains(valSearch.ToLower()) && e.loai == filter && e.SelectedProvinceValue == SelectedProvinceValue).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                }

                            }
                            else  // Chọn lọc tất cả
                            {
                                list = list.Where(e => e.TT_GiayTo.ToLower().Contains(valSearch.ToLower()) && e.loai == filter).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                            }
                        }

                    }
                    else      // Else  tất cả  ( filter==0 )
                    {
                        list = list.Where(e => e.TT_GiayTo.ToLower().Contains(valSearch.ToLower())).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                    }
                }
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListControl", list);
                }
                else
                {
                    return View("Index", list);
                }
            }
            ///////////////////////////////////////////////
            else
            {
                {
                    if (filter != 0)
                    {
                        if (filter == 4)                    // Nếu chọn đang duyệt
                        {
                            if (SelectedProvinceValue != 0)// Neu Chon Tinh Khac 0
                            {
                                if (chkDistrict == true)    // Chọn Cấp Quận
                                {

                                    if (chkDetail == true) // Chọn cấp phường
                                    {
                                        list = list.Where(e => e.TT_FullName.ToLower().Contains(valSearch.ToLower()) && e.SelectedStatusValue == 3 && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedDistrictValue == SelectedDistrictValue && e.SelectedDetailProvincesValue == SelectedDetailProvincesValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                    }
                                    else           // Else phường -> Chọn Quận
                                    {
                                        list = list.Where(e => e.TT_FullName.ToLower().Contains(valSearch.ToLower()) && e.SelectedStatusValue == 3 && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedDistrictValue == SelectedDistrictValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                    }
                                }
                                else    // else Quận Chọn tỉnh
                                {
                                    list = list.Where(e => e.TT_FullName.ToLower().Contains(valSearch.ToLower()) && e.SelectedStatusValue == 3 && e.SelectedProvinceValue == SelectedProvinceValue).OrderBy(e => e.TT_FullName).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                }

                            }

                        }
                        else
                        {
                            if (SelectedProvinceValue != 0)       // Neu Chon Tinh Khac 0
                            {
                                if (chkDistrict == true)    // Chọn Cấp Quận
                                {

                                    if (chkDetail == true) // Chọn cấp phường
                                    {
                                        list = list.Where(e => e.TT_FullName.ToLower().Contains(valSearch.ToLower()) && e.loai == filter && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedDistrictValue == SelectedDistrictValue && e.SelectedDetailProvincesValue == SelectedDetailProvincesValue).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                    }

                                    else       //Chọn quận
                                    {
                                        list = list.Where(e => e.TT_FullName.ToLower().Contains(valSearch.ToLower()) && e.loai == filter && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedProvinceValue == SelectedProvinceValue && e.SelectedDistrictValue == SelectedDistrictValue).ToList().ToPagedList(currentPageIndex, defaultPageSize);

                                    }
                                }
                                else   //else cấp quận
                                {
                                    list = list.Where(e => e.TT_FullName.ToLower().Contains(valSearch.ToLower()) && e.loai == filter && e.SelectedProvinceValue == SelectedProvinceValue).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                                }
                            }
                            else   // Else Chọn Tỉnh =0
                            {
                                list = list.Where(e => e.TT_FullName.ToLower().Contains(valSearch.ToLower()) && e.loai == filter).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                            }

                        }
                    }

                    else
                    {

                        list = list.Where(e => e.TT_FullName.ToLower().ToLower().Contains(valSearch.ToLower())).ToList().ToPagedList(currentPageIndex, defaultPageSize);
                    }
                }
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListControl", list);
                }
                else
                {
                    return View("Index", list);
                }
            }


        }
        //
        // GET: /TamTru/Details/5
            [Authorize(Roles = "Admin")]
           public ViewResult Details(int id)
           {
               TamTruModel model = access.getTTTVByID(id);
               return View(model);
           }

        //
        // GET: /TamTru/Create
           [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewData["GiayTo"] = new SelectList(proxy.getAllGiayTo(), "ID_GiayTo", "GiayTo");
            ViewData["LiDo"] = new SelectList(proxy.getAllLiDo(), "ID_LiDo", "LiDo");
            ViewData["QuocTich"] = new SelectList(proxy.getAllQuocTich(), "ID_QuocTich", "QuocTich");
            ViewData["Tinh"] = new SelectList(proxy.getAllProvince(), "ProvinceID", "ProvinceName");
            ViewData["Quan"] = new SelectList(proxy.getAllDistrict(), "DistrictID", "DistrictName");
            ViewData["Phuong"] = new SelectList(proxy.getAllDetailProvince(), "DetailID", "DetailName");
            return View();
        } 

        //
        // POST: /TamTru/Create
         [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(TamTruModel model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name.ToString();
                proxy.insertAtPolice(model.SelectedGiayToValue, model.SelectedLiDoValue, model.SelectedQuocTichValue, model.SelectedProvinceValue, model.SelectedDistrictValue, model.SelectedDetailProvincesValue, model.TT_FullName, model.TT_DiaChiThuongTru, model.TT_DiaChiTamTru, model.TT_NgayDen, model.TT_NgayDi, model.TT_Room, model.TT_GiayTo, model.TT_LiDoKhac, user);
                return RedirectToAction("Index");
            }
            ViewData["GiayTo"] = new SelectList(proxy.getAllGiayTo(), "ID_GiayTo", "GiayTo");
            ViewData["LiDo"] = new SelectList(proxy.getAllLiDo(), "ID_LiDo", "LiDo");
            ViewData["QuocTich"] = new SelectList(proxy.getAllQuocTich(), "ID_QuocTich", "QuocTich");
            ViewData["Tinh"] = new SelectList(proxy.getAllProvince(), "ProvinceID", "ProvinceName");
            ViewData["Quan"] = new SelectList(proxy.getAllDistrict(), "DistrictID", "DistrictName");
            ViewData["Phuong"] = new SelectList(proxy.getAllDetailProvince(), "DetailID", "DetailName");
            return View(model);
        }
        
        //
        // GET: /TamTru/Edit/5
         [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            TamTruModel model = access.getTTTVByID(id);
            ViewData["GiayTo"] = new SelectList(proxy.getAllGiayTo(), "ID_GiayTo", "GiayTo", model.SelectedGiayToValue);
            ViewData["LiDo"] = new SelectList(proxy.getAllLiDo(), "ID_LiDo", "LiDo", model.SelectedLiDoValue);
            ViewData["QuocTich"] = new SelectList(proxy.getAllQuocTich(), "ID_QuocTich", "QuocTich", model.SelectedQuocTichValue);
            ViewData["Status"] = new SelectList(proxy.getAllStatus(), "ID_Status", "Status", model.SelectedStatusValue);
            ViewData["Type"] = new SelectList(proxy.getAllType(), "ID_Type", "Type", model.SelectedTypeValue);
            return View(model);
        }

        //
        // POST: /TamTru/Edit/5
         [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(TamTruModel model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name.ToString();
                try
                {
                    proxy.Updatetamtru(model.MaTamTru, model.SelectedTypeValue, model.SelectedGiayToValue, model.SelectedLiDoValue, model.SelectedQuocTichValue, model.SelectedStatusValue, model.TT_FullName, model.TT_DiaChiThuongTru, model.TT_DiaChiTamTru, model.TT_NgayDen, model.TT_NgayDi, model.TT_Room, model.TT_GiayTo, model.TT_LiDoKhac, user);
                }
                 catch(FaultException<FaultOutOfMemory> cex)
                {
                    FaultModel fault = new FaultModel();
                    fault.Issue = cex.Message;
                    fault.Detail = cex.Reason.ToString() ;
                    return View("fault", fault);
                }

                return RedirectToAction("Details", new { id = model.MaTamTru });
            }
            ViewData["GiayTo"] = new SelectList(proxy.getAllGiayTo(), "ID_GiayTo", "GiayTo", model.SelectedGiayToValue);
            ViewData["LiDo"] = new SelectList(proxy.getAllLiDo(), "ID_LiDo", "LiDo", model.SelectedLiDoValue);
            ViewData["QuocTich"] = new SelectList(proxy.getAllQuocTich(), "ID_QuocTich", "QuocTich", model.SelectedQuocTichValue);
            ViewData["Status"] = new SelectList(proxy.getAllStatus(), "ID_Status", "Status", model.SelectedStatusValue);
            ViewData["Type"] = new SelectList(proxy.getAllType(), "ID_Type", "Type", model.SelectedTypeValue);
            return View();
        }

        //
        // GET: /TamTru/Delete/5
         [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            TamTruModel model = access.getTTTVByID(id);
            return View(model);
        }

        //
        // POST: /TamTru/Delete/5

           [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            proxy.deleteTamTru(id);
            return RedirectToAction("Index");
        }
          [Authorize(Roles = "NormalUser")]
        public ActionResult CreateByUser()
        {
            ViewData["GiayTo"] = new SelectList(proxy.getAllGiayTo(), "ID_GiayTo", "GiayTo");
            ViewData["LiDo"] = new SelectList(proxy.getAllLiDo(), "ID_LiDo", "LiDo");
            ViewData["QuocTich"] = new SelectList(proxy.getAllQuocTich(), "ID_QuocTich", "QuocTich");
            return View();
        }
        [Authorize(Roles = "NormalUser")]
        [HttpPost]
        public ActionResult CreateByUser(TamTruModelUser model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name.ToString();
                proxy.insertbyUser(user, model.SelectedGiayToValue, model.SelectedLiDoValue, model.SelectedQuocTichValue, model.TT_DiaChiTamTru, model.TT_NgayDen, model.TT_NgayDi, model.TT_Room, model.TT_GiayTo, model.TT_LiDoKhac);
                return View("Success1");
            }
            ViewData["GiayTo"] = new SelectList(proxy.getAllGiayTo(), "ID_GiayTo", "GiayTo", model.SelectedGiayToValue);
            ViewData["LiDo"] = new SelectList(proxy.getAllLiDo(), "ID_LiDo", "LiDo", model.SelectedLiDoValue);
            ViewData["QuocTich"] = new SelectList(proxy.getAllQuocTich(), "ID_QuocTich", "QuocTich", model.SelectedQuocTichValue);
            return View(model);
        }
         [Authorize(Roles = "Admin")]
        public ActionResult Duyet(int id, string user)
        {
            proxy.Duyet(id);
            string email = proxy.Email(user);
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "anhquan1989@gmail.com";
            WebMail.Password = "10a09q05";
            WebMail.From = "anhquan1989@gmail.com";
            string content = "Chúc mừng bạn đã đăng ký thành công";
            WebMail.Send(email, "Dang Ky  tam Tru Tam Vang", content);
            return View("Success");
        }
         [Authorize(Roles = "NormalUser")]
        public ActionResult History(string username)
        {
            if (access.chkHistory(username))
            {
                IList<TamTruModel> model = access.getHistory(username);
                return View(model);
            }
            else
                return View("Sorry");
        }
        protected override void Dispose(bool disposing)
        {
            proxy.Dispose();
            base.Dispose(disposing);
        }
    }
}
