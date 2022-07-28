using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNP_SHOP.Models;

namespace TNP_SHOP.Controllers
{
    public class QuanLyController : Controller
    {
        //
        // GET: /QuanLy/

        QLDTDataContext db = new QLDTDataContext();
        public ActionResult DanhMucPartial()
        {
            return View();
        }

        public ActionResult QuanLyLoai()
        {
            return View();
        }

        public ActionResult QuanLySanPham()
        {
            return View();
        }
        public ActionResult QuanLyHoaDon()
        {
            return View();
        }
        public ActionResult QuanLyChiTietHD()
        {
            return View();
        }

        public ActionResult LoaiPartial()
        {
            List<Loai> lstloai = db.Loais.OrderBy(t => t.MaLoai).ToList();
            return View(lstloai);
        }

        public ActionResult SanPhamPartial()
        {
            List<SANPHAM> lstSanPham = db.SANPHAMs.OrderBy(t => t.MaHang).ToList();
            return View(lstSanPham);
        }

        public ActionResult HoaDonPartial()
        {
            List<HOADON> lstHoaDon = db.HOADONs.OrderBy(t => t.MaHD).ToList();
            return View(lstHoaDon);
        }

        public ActionResult CTHoaDonPartial()
        {
            List<CHITIETHD> lstcthd = db.CHITIETHDs.OrderBy(t => t.MaHD).ToList();
            return View(lstcthd);
        }

        public ActionResult XoaLoai(int maloai)
        {
            Loai loai = db.Loais.Single(t => t.MaLoai == maloai);
            List<SANPHAM> lstsp = db.SANPHAMs.Where(t => t.MaLoai == maloai).ToList();
            if (lstsp.Count > 0)
            {
                ViewData["tb"] = "Vui lòng xóa sản phẩm có mã này trước!!!";
                Response.Write("<script>alert('Vui lòng xóa sản phẩm có mã này trước!!!')</script>");
            }
            else 
            {
                if (loai != null)
                {
                    db.Loais.DeleteOnSubmit(loai);
                    db.SubmitChanges();
                    ViewData["tb"] = "Xóa thành công!!!";
                }
            }
            return RedirectToAction("QuanLyLoai", "QuanLy");
        }

        public ActionResult ChiTietLoai(int maloai)
        {
            List<SANPHAM> lstsp = db.SANPHAMs.Where(t => t.MaLoai == maloai).ToList();
            return View(lstsp);
        }

        public ActionResult XoaSanPham(int mahang)
        {
            SANPHAM sp = db.SANPHAMs.Single(t => t.MaHang == mahang);
            if (sp != null)
            {
                db.SANPHAMs.DeleteOnSubmit(sp);
                db.SubmitChanges();
            }
            return RedirectToAction("QuanLySanPham", "QuanLy");
        }

        public ActionResult CapNhatSanPham(int mahang,FormCollection f)
        {
            SANPHAM sp = db.SANPHAMs.Single(t => t.MaHang == mahang);
            if (sp != null)
            {
                sp.Gia = int.Parse(f["txtGia"]);
                db.SubmitChanges();
            }
            return RedirectToAction("QuanLySanPham", "QuanLy");
        }

        public ActionResult XoaHoaDon(int mahd)
        {
            HOADON hd = db.HOADONs.Single(t => t.MaHD == mahd);
            List<CHITIETHD> lstcthd = db.CHITIETHDs.Where(t => t.MaHD == mahd).ToList();
            if (lstcthd.Count > 0)
            {
                ViewBag.tb = "Vui lòng xóa sản phẩm có mã này trước!!!";
                Response.Write("<script>alert('Vui lòng xóa sản phẩm có mã này trước!!!')</script>");
            }
            else
            {
                if (hd != null)
                {
                    db.HOADONs.DeleteOnSubmit(hd);
                    db.SubmitChanges();
                }
            }
            return RedirectToAction("QuanLyHoaDon", "QuanLy");
        }

        public ActionResult ChiTietHoaDon(int mahd)
        {
            List<CHITIETHD> lstsp = db.CHITIETHDs.Where(t => t.MaHD == mahd).ToList();
            return View(lstsp);
        }


        public ActionResult XoaCTHD(int mahd,string tenhang)
        {
            
            CHITIETHD cthd = db.CHITIETHDs.Single(t => t.MaHD == mahd && t.TenHang == tenhang);
            HOADON hd = db.HOADONs.Single(t => t.MaHD == mahd);
           if (cthd != null)
           {
               db.CHITIETHDs.DeleteOnSubmit(cthd);
               db.SubmitChanges();
               List<CHITIETHD> lst = db.CHITIETHDs.Where(t => t.MaHD == mahd).ToList();
               hd.TongTien = lst.Sum(t => t.ThanhTien);
               db.SubmitChanges();
           }
            return RedirectToAction("QuanLyChiTietHD", "QuanLy");
        }
    }
}
