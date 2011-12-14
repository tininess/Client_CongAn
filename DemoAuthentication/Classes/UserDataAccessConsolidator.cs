using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DemoAuthentication.Models;
using DemoAuthentication.ServiceReference1;

namespace DemoAuthentication.Classes
{
    public class UserDataAccessConsolidator
    {

        Level2ServiceClient proxy = new Level2ServiceClient();
    
     
        public IList<UserModel> getUserList()               //New
        {
            IList<TTTV__UserInformation> data = proxy.getUserList();
            IList<UserModel> list = new List<UserModel>();
            foreach (var item in data)
            {
                UserModel model = new UserModel();
                model.UserName = item.Username;
                model.SelectedDetailProvincesValue = item.ProvinceDetailID;
                model.SelectedDistrictValue = item.DistrictID;
                model.SelectedProvinceValue = item.ProvinceID;
                model.Tinh = proxy.Tinh(item.ProvinceID);
                model.Quan = proxy.Quan(item.ProvinceID, item.DistrictID);
                model.Phuong = proxy.Phuong(item.ProvinceID, item.DistrictID, item.ProvinceDetailID);
                model.Companyname = item.CompanyName;
                model.BusinessCode = item.BusinessCode;
                model.Phone = item.Phone;
                model.Mobile = item.Mobile;
                model.CMND = item.PersonCode;
                model.Name = item.UserFullName;
                model.Age = item.Age;
                model.Address = item.Address;
                model.subAddress = item.SubAddress;
                list.Add(model);
            }
            return list;

        }


        public UserModel getUserById(string username)     // New
        {
            TTTV__UserInformation item = proxy.getUserInfoByID(username);
            if (item != null)
            {
                UserModel model = new UserModel();

                model.UserName = item.Username;
                model.SelectedDetailProvincesValue = item.ProvinceDetailID;
                model.SelectedDistrictValue = item.DistrictID;
                model.SelectedProvinceValue = item.ProvinceID;
                model.Tinh = proxy.Tinh(item.ProvinceID);
                model.Quan = proxy.Quan(item.ProvinceID, item.DistrictID);
                model.Phuong = proxy.Phuong(item.ProvinceID, item.DistrictID, item.ProvinceDetailID);
                model.Companyname = item.CompanyName;
                model.BusinessCode = item.BusinessCode;
                model.Phone = item.Phone;
                model.Mobile = item.Mobile;
                model.CMND = item.PersonCode;
                model.Name = item.UserFullName;
                model.Age = item.Age;
                model.Address = item.Address;
                model.subAddress = item.SubAddress;
                return model;

            }
            else return null;

        }


        public UserLoginModel getUserLoginById(string username)       //New
        {
            TTTV__Users item = proxy.getUserLogin(username);
            if (item != null)
            {
                UserLoginModel model = new UserLoginModel();

                model.UserName = item.Username;
                model.Email = item.Email;
                model.roleID = item.RoleId;

                return model;

            }
            else return null;

        }



        


    }
}