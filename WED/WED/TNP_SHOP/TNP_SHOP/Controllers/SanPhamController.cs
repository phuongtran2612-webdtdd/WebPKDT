using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNP_SHOP.Models;
namespace TNP_SHOP.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/

        QLDTDataContext db = new QLDTDataContext();
        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index3()
        {
            return View();
        }
        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }

        public ActionResult LienHeQuanLy()
        {
            return View();
        }

        public ActionResult SanPhamPartial()
        {
            return View(db.SANPHAMs.OrderBy(t => t.MaHang));
        }

        public ActionResult TrangChuQuanLy()
        {
            return View(db.SANPHAMs.OrderBy(t => t.MaHang));
        }

        public ActionResult DienThoaiPartial()
        {
            return View(db.Loais.Where(t => t.Note.Trim() == "DT"));
        }

        public ActionResult PhuKienPartial()
        {
            return View(db.Loais.Where(t => t.Note.Trim() == "PK"));
        }

        public ActionResult DienThoaiPartialQuanLy()
        {
            return View(db.Loais.Where(t => t.Note.Trim() == "DT"));
        }

        public ActionResult PhuKienPartialQuanLy()
        {
            return View(db.Loais.Where(t => t.Note.Trim() == "PK"));
        }

        public ActionResult SanPhamTheoLoai(int maLoai)
        {
            var dsloai = db.Loais.SingleOrDefault(t => t.MaLoai == maLoai);
            string loai = dsloai.TenLoai.ToString();
            var listSPTheoLoai = db.SANPHAMs.Where(t => t.MaLoai == maLoai).OrderBy(t => t.MaHang).ToList();
            @ViewBag.loai = loai;
            return View(listSPTheoLoai);
        }

        public ActionResult SanPhamTheoLoaiQuanLy(int maLoai)
        {
            var dsloai = db.Loais.SingleOrDefault(t => t.MaLoai == maLoai);
            string loai = dsloai.TenLoai.ToString();
            var listSPTheoLoai = db.SANPHAMs.Where(t => t.MaLoai == maLoai).OrderBy(t => t.MaHang).ToList();
            @ViewBag.loai = loai;
            return View(listSPTheoLoai);
        }

        public ActionResult XemChiTiet(int msp)
        {
            return View(db.SANPHAMs.SingleOrDefault(t => t.MaHang == msp));
        }
        public ActionResult XemChiTietQuanLy(int msp)
        {
            return View(db.SANPHAMs.SingleOrDefault(t => t.MaHang == msp));
        }

        public ActionResult TimKiem(FormCollection col)
        {
            //var lstSearch = db.SANPHAMs.Where(t => t.MaHang.ToString().Contains(txt_Search) || t.TenHang.Contains(txt_Search) || t.MaLoai.ToString().Contains(txt_Search));
            //return View(lstSearch.ToList());
            
            string tk = col["txt_Search"].ToString();
            @ViewBag.timkiem = tk;
            List<SANPHAM> dsS = db.SANPHAMs.Where(s => s.TenHang.Contains(tk) == true).ToList();
            if (dsS.Count == 0)
            {
                return View("Index2", dsS);
            }
            else
            return View(dsS);
        }

        public ActionResult TimKiemQuanLy(FormCollection col)
        {
            //var lstSearch = db.SANPHAMs.Where(t => t.MaHang.ToString().Contains(txt_Search) || t.TenHang.Contains(txt_Search) || t.MaLoai.ToString().Contains(txt_Search));
            //return View(lstSearch.ToList());

            string tk = col["txt_Search"].ToString();
            @ViewBag.timkiem = tk;
            List<SANPHAM> dsS = db.SANPHAMs.Where(s => s.TenHang.Contains(tk) == true).ToList();
            if (dsS.Count == 0)
            {
                return View("Index4", dsS);
            }
            else
                return View(dsS);
        }
    }
}
