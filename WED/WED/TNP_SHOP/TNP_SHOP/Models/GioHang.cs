using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TNP_SHOP.Models;
namespace TNP_SHOP.Models
{
    public class GioHang
    {
        QLDTDataContext db = new QLDTDataContext();
        public int iMaSP { get; set; }

        public string sTenSP { get; set; }

        public string sAnh { get; set; }

        public int iSoLuong { get; set; }

        public int dDonGia { get; set; }

        public int dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        //Khởi tạo giỏ hàng
        public GioHang(int maSP)
        {
            SANPHAM sp = db.SANPHAMs.Single(s => s.MaHang == maSP);
            iMaSP = sp.MaHang;
            sTenSP = sp.TenHang;
            sAnh = sp.Hinh;
            dDonGia = int.Parse(sp.Gia.ToString());
            iSoLuong = 1;
        }
    }
}