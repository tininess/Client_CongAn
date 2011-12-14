using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoAuthentication.ServiceReference1;
using DemoAuthentication.Models;

namespace DemoAuthentication.Classes
{
    public class TamTruAccessConsolidator

    {
       
        Level2ServiceClient proxy = new Level2ServiceClient();
        public IList<TamTruModel> getAllTTTV()
        {

            IList<TamTruModel> list = new List<TamTruModel>();
            var data = proxy.getAllTamTru();
            foreach (var item in data)
            {
                TamTruModel model = new TamTruModel();
                model.MaTamTru = item.ID_TamTru;
                model.loai = item.ID_Type;
                model.Tinh = proxy.Tinh(item.ProvinceID);
                model.Quan = proxy.Quan(item.ProvinceID,item.DistrictID);
                model.Phuong = proxy.Phuong(item.ProvinceID,item.DistrictID,item.DetailProvinceID);
                model.SelectedDetailProvincesValue = item.DetailProvinceID;
                model.SelectedDistrictValue = item.DistrictID;
                model.SelectedProvinceValue = item.ProvinceID;
                model.SelectedStatusValue = item.ID_Status;
                model.Type = item.TTTV__Type.Type;
                model.GiayTo = item.TTTV__GiayTo.GiayTo;
                model.TrangThai = item.TTTV__Status.Status;
                model.QuocTich = item.TTTV__QuocTich.QuocTich;
                model.LiDo = item.TTTV__LiDo.LiDo;
                model.TT_FullName = item.TT_FullName;
                model.TT_NgayDen = item.TT_NgayDen;
                model.TT_NgayDi = item.TT_NgayDi;
                model.TT_DiaChiThuongTru = item.TT_DiaChiThuongTru;
                model.TT_DiaChiTamTru = item.TT_DiaChiTamTru;
                model.TT_Room = item.TT_Room;
                model.TT_GiayTo = item.TT_GiayTo;
                model.username = item.TaiKhoanDangKi;
                list.Add(model);
            }
            return list;
        }
        public TamTruModel getTTTVByID(int id)
        {
            TTTV__TamTru item = proxy.getTamTruById(id);
            TamTruModel model = new TamTruModel();

            model.MaTamTru = item.ID_TamTru;
            model.loai = item.ID_Type;
            model.Tinh = proxy.Tinh(item.ProvinceID);
            model.Quan = proxy.Quan(item.ProvinceID, item.DistrictID);
            model.Phuong = proxy.Phuong(item.ProvinceID, item.DistrictID, item.DetailProvinceID);
            model.SelectedDetailProvincesValue = item.DetailProvinceID;
            model.SelectedDistrictValue = item.DistrictID;
            model.SelectedProvinceValue = item.ProvinceID;
            model.SelectedTypeValue = item.ID_Type;
            model.SelectedLiDoValue = item.ID_LiDo;
            model.SelectedQuocTichValue = item.ID_QuocTich;
            model.SelectedGiayToValue = item.ID_GiayTo;
            model.SelectedStatusValue = item.ID_Status;
            model.Type = item.TTTV__Type.Type;
            model.GiayTo = item.TTTV__GiayTo.GiayTo;
            model.TrangThai = item.TTTV__Status.Status;
            model.QuocTich = item.TTTV__QuocTich.QuocTich;
            model.LiDo = item.TTTV__LiDo.LiDo;
            model.TT_FullName = item.TT_FullName;
            model.TT_NgayDen = item.TT_NgayDen;
            model.TT_NgayDi = item.TT_NgayDi;
            model.TT_DiaChiThuongTru = item.TT_DiaChiThuongTru;
            model.TT_DiaChiTamTru = item.TT_DiaChiTamTru;
            model.TT_Room = item.TT_Room;
            model.TT_GiayTo = item.TT_GiayTo;
            model.TT_LiDoKhac = item.TT_LiDoKhac;
            model.username = item.TaiKhoanDangKi;
            return model;

        }

        public bool chkHistory(string username)
        {
            var list = proxy.getHistory(username);
            if (list != null) return true;
            return false;
        }
        public IList<TamTruModel> getHistory(string username)
        {
            IList<TamTruModel> list = new List<TamTruModel>();
            var data = proxy.getHistory(username);
            
                foreach (var item in data)
                {
                    TamTruModel model = new TamTruModel();
                    model.MaTamTru = item.ID_TamTru;
                    model.loai = item.ID_Type;
                    model.Tinh = proxy.Tinh(item.ProvinceID);
                    model.Quan = proxy.Quan(item.ProvinceID, item.DistrictID);
                    model.Phuong = proxy.Phuong(item.ProvinceID, item.DistrictID, item.DetailProvinceID);
                    model.SelectedDetailProvincesValue = item.DetailProvinceID;
                    model.SelectedDistrictValue = item.DistrictID;
                    model.SelectedProvinceValue = item.ProvinceID;
                    model.SelectedStatusValue = item.ID_Status;
                    model.Type = item.TTTV__Type.Type;
                    model.GiayTo = item.TTTV__GiayTo.GiayTo;
                    model.TrangThai = item.TTTV__Status.Status;
                    model.QuocTich = item.TTTV__QuocTich.QuocTich;
                    model.LiDo = item.TTTV__LiDo.LiDo;
                    model.TT_FullName = item.TT_FullName;
                    model.TT_NgayDen = item.TT_NgayDen;
                    model.TT_NgayDi = item.TT_NgayDi;
                    model.TT_DiaChiThuongTru = item.TT_DiaChiThuongTru;
                    model.TT_DiaChiTamTru = item.TT_DiaChiTamTru;
                    model.TT_Room = item.TT_Room;
                    model.TT_GiayTo = item.TT_GiayTo;
                    model.username = item.TaiKhoanDangKi;
                    list.Add(model);
                }
                return list;
            }
    }
}