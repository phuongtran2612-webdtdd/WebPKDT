using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNP_SHOP.Models;

namespace TNP_SHOP.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        QLDTDataContext db = new QLDTDataContext();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //nếu lstGioHang  ch tồn tại thì khởi tạo
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int msp, string strURL)
        {
            //lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sách có tồn tại trong sesson["GioHang"] chưa?
            GioHang SanPham = lstGioHang.Find(sp => sp.iMaSP == msp);
            if (SanPham == null)
            {
                SanPham = new GioHang(msp);
                lstGioHang.Add(SanPham);
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //tổng số lượng
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl += lstGioHang.Sum(sp => sp.iSoLuong);
            }
            return tsl;
        }
        //tổng thành tiền
        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                ttt += lstGioHang.Sum(sp => sp.dThanhTien);
            }
            return ttt;
        }
        //xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index1", "SanPham");
            }
            List<GioHang> lstGioHang = LayGioHang();

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();

            return View(lstGioHang);
        }

        public ActionResult GioHangQuanLy()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index3", "SanPham");
            }
            List<GioHang> lstGioHang = LayGioHang();

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();

            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return PartialView();
        }

        public ActionResult GioHangPartialQuanLy()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return PartialView();
        }
        //xoá giỏ hàng
        public ActionResult XoaGioHang(int MaSP)
        {
            //lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sách cần xoá có trog giỏ hàng ko?
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);

            //nếu có thì xoá
            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.iMaSP == MaSP);
                return RedirectToAction("GioHang", "GioHang");
            }
            //nếu giỏ hàng rỗng
            if (lstGioHang.Count==0)
            {
                return RedirectToAction("Index1", "SanPham");
            }
            return RedirectToAction("GioHang", "GioHang");
        }
        public ActionResult XoaGioHang_ALL()
        {
            //lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            LayGioHang().Clear();
            return RedirectToAction("Index1", "SanPham");
        }
        public ActionResult CapNhatGioHang(int MaSP, FormCollection f)
        {
            //lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sách cần xoá có trog giỏ hàng ko?
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang", "GioHang");
        }


        public ActionResult XoaGioHangQuanLy(int MaSP)
        {
            //lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sách cần xoá có trog giỏ hàng ko?
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);

            //nếu có thì xoá
            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.iMaSP == MaSP);
                return RedirectToAction("GioHangQuanLy", "GioHang");
            }
            //nếu giỏ hàng rỗng
            if (lstGioHang.Count == 0)
            {
                RedirectToAction("Index3", "SanPham");
            }
            return RedirectToAction("GioHangQuanLy", "GioHang");
            
        }
        public ActionResult XoaGioHang_ALL_QuanLy()
        {
            //lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            LayGioHang().Clear();
            return RedirectToAction("Index3", "SanPham");
        }
        public ActionResult CapNhatGioHangQuanLy(int MaSP, FormCollection f)
        {
            //lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sách cần xoá có trog giỏ hàng ko?
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHangQuanLy", "GioHang");
        }
        public ActionResult ThanhToan()
        {
            var us = Session["User"] as TAIKHOAN;
            List<GioHang> listCart = Session["GioHang"] as List<GioHang>;
            if (us == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                if (listCart == null)
                {
                    Response.Write("<script>alert('Giỏ Hàng hiện đang trống!!!')</script>");
                }
                else
                {
                    //tao hoa don
                    HOADON hoadon = new HOADON();
                    hoadon.Username = us.Username;
                    hoadon.NguoiDat = us.Ten;
                    hoadon.DiaChi = "Gia Lai";
                    hoadon.TongTien = listCart.Sum(t => t.dThanhTien);
                    hoadon.NgayXuatHD = DateTime.Now.ToString();
                    db.HOADONs.InsertOnSubmit(hoadon);
                    db.SubmitChanges();
                    List<HOADON> lsthd = db.HOADONs.OrderByDescending(t => t.MaHD).ToList();
                    var hdcc = lsthd.First();
                    int mhd = hdcc.MaHD;


                    //them chi tiet
                    //copy gio hang vào ct hoa don
                    foreach (GioHang item in listCart)
                    {
                        CHITIETHD ct = new CHITIETHD();
                        ct.MaHD = mhd;
                        ct.TenHang = item.sTenSP;
                        ct.SoLuong = item.iSoLuong;
                        ct.ThanhTien = item.dThanhTien;

                        // insert vao database

                        db.CHITIETHDs.InsertOnSubmit(ct);
                        db.SubmitChanges();
                    }
                    // thanh toan thanh cong
                    List<GioHang> list = LayGioHang();
                    list.Clear();
                    Response.Write("<script>alert('Thanh toán thành công!!!')</script>");
                }
            }
            return RedirectToAction("Index1", "SanPham");

        }

        public ActionResult ThanhToanQuanLy()
        {
            var us = Session["User"] as TAIKHOAN;
            List<GioHang> listCart = Session["GioHang"] as List<GioHang>;
            if (us == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                if (listCart == null)
                {
                    Response.Write("<script>alert('Giỏ Hàng hiện đang trống!!!')</script>");
                }
                else
                {
                //tao hoa don
                HOADON hoadon = new HOADON();
                hoadon.Username = us.Username;
                hoadon.NguoiDat = us.Ten;
                hoadon.DiaChi = "Gia Lai";
                hoadon.TongTien = listCart.Sum(t => t.dThanhTien);
                hoadon.NgayXuatHD = DateTime.Now.ToString();
                db.HOADONs.InsertOnSubmit(hoadon);
                db.SubmitChanges();
                List<HOADON> lsthd = db.HOADONs.OrderByDescending(t => t.MaHD).ToList();
                var hdcc = lsthd.First();
                int mhd = hdcc.MaHD;


                //them chi tiet
                //copy gio hang vào ct hoa don
                foreach (GioHang item in listCart)
                {
                    CHITIETHD ct = new CHITIETHD();
                    ct.MaHD = mhd;
                    ct.TenHang = item.sTenSP;
                    ct.SoLuong = item.iSoLuong;
                    ct.ThanhTien = item.dThanhTien;

                    // insert vao database

                    db.CHITIETHDs.InsertOnSubmit(ct);
                    db.SubmitChanges();
                }
                // thanh toan thanh cong
                List<GioHang> list = LayGioHang();
                list.Clear();
                Response.Write("<script>alert('Thanh toán thành công!!!')</script>");
            }
                }
            return RedirectToAction("Index3", "SanPham");
        }
    }
}
