using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNP_SHOP.Models;

namespace TranTrongBinhPhuong_2001190760_KTL23.Controllers
{
    public class TaiKhoanController : Controller
    {
        QLDTDataContext db = new QLDTDataContext();
        //
        // GET: /TaiKhoan/
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(TAIKHOAN tk, FormCollection f)
        {
            //gán các giá trị người dùng nhập từ form f cho các biến
            var username = f["Username"];
            var matkhau = f["Pass"];
            var remathau = f["ReMatKhau"];
            var hoten = f["Ten"];
            var email = f["Email"];
            var dienthoai = f["SDT"];
            var gioitinh = f["Gioitinh"];

            if (String.IsNullOrEmpty(username))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được bỏ trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }
            if (String.IsNullOrEmpty(remathau))
            {
                ViewData["Loi3"] = "Vui lòng xác nhận mật khẩu";
            }
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi4"] = "Họ Tên không được bỏ trống";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Vui lòng nhập số điện thoại";
            }
            if (String.IsNullOrEmpty(gioitinh))
            {
                ViewData["Loi7"] = "Giới tính không được bỏ trống";
            }
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(matkhau) && !String.IsNullOrEmpty(remathau))
            {
                //gán giá trị cho đối tượng kh
                tk.Username = username;
                tk.Pass = matkhau;
                tk.Ten = hoten;
                tk.Email = email;
                tk.SDT = dienthoai;
                tk.Gioitinh = gioitinh;
                tk.Quyen = 1;
                db.TAIKHOANs.InsertOnSubmit(tk);
                db.SubmitChanges();
                //Response.Write("<script>alert('Đăng Ký Thành Công!!!')</script>");
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                Response.Write("<script>alert('Tên đăng nhập đã tồn tại!!!')</script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult DangNhap(FormCollection f)
        {
            //khai báo cac biến nhận gtri tu form f
            var username = f["Username"];
            var matkhau = f["Pass"];

            if (String.IsNullOrEmpty(username))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được bỏ trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(matkhau))
            {
                TAIKHOAN tk = db.TAIKHOANs.SingleOrDefault(c => c.Username == username && c.Pass == matkhau);
                if (tk != null)
                {
                    Session["User"] = tk;
                    Session["tdn"] = tk.Username;
                    if (tk.Quyen == 2)
                    {
                        return RedirectToAction("TrangChuQuanLy", "SanPham");
                    }
                    else
                    {
                        return RedirectToAction("SanPhamPartial", "SanPham");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Tài Khoản hoặc mật khẩu không đúng !!!')</script>");
                }
               
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["User"] = null;
            Session["tdn"] = null;
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f)
        {
            string username = Session["tdn"].ToString();
            TAIKHOAN tk = db.TAIKHOANs.Single(t => t.Username == username);
            var passCu = f["PassCu"];
            var PassMoi = f["PassMoi"];
            var RePass = f["RePass"];
            var pass = f["Pass"];
            if (String.IsNullOrEmpty(passCu))
            {
                ViewData["Loi1"] = "Vui lòng nhập mật khẩu cũ!";
            }
            if (tk.Pass.Equals(passCu)==false)
            {
                ViewData["Loi1"] = "Mật khẩu cũ không đúng !";
            }
            if (String.IsNullOrEmpty(PassMoi))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu mới";
            }
            if (String.IsNullOrEmpty(RePass))
            {
                ViewData["Loi3"] = "Vui lòng nhập lại mật khẩu";
            }
            if (PassMoi.Equals(RePass) == false)
            {
                ViewData["Loi3"] = "Mật khẩu nhập lại không đúng!";
            }
            if (tk.Pass.Equals(passCu) && PassMoi.Equals(RePass) && !String.IsNullOrEmpty(PassMoi) && !String.IsNullOrEmpty(RePass))
            {
                
                tk.Pass = PassMoi.ToString();
                db.SubmitChanges(); 
                return RedirectToAction("SanPhamPartial", "SanPham");
            }
            else
            {
                return View();
            } 
        }

        [HttpGet]
        public ActionResult DoiMatKhauQuanLy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoiMatKhauQuanLy(FormCollection f)
        {
            string username = Session["tdn"].ToString();
            TAIKHOAN tk = db.TAIKHOANs.Single(t => t.Username == username);
            var passCu = f["PassCu"];
            var PassMoi = f["PassMoi"];
            var RePass = f["RePass"];
            var pass = f["Pass"];
            if (String.IsNullOrEmpty(passCu))
            {
                ViewData["Loi1"] = "Vui lòng nhập mật khẩu cũ!";
            }
            if (tk.Pass.Equals(passCu) == false)
            {
                ViewData["Loi1"] = "Mật khẩu cũ không đúng !";
            }
            if (String.IsNullOrEmpty(PassMoi))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu mới";
            }
            if (String.IsNullOrEmpty(RePass))
            {
                ViewData["Loi3"] = "Vui lòng nhập lại mật khẩu";
            }
            if (PassMoi.Equals(RePass) == false)
            {
                ViewData["Loi3"] = "Mật khẩu nhập lại không đúng!";
            }
            if (tk.Pass.Equals(passCu) && PassMoi.Equals(RePass) && !String.IsNullOrEmpty(PassMoi) && !String.IsNullOrEmpty(RePass))
            {

                tk.Pass = PassMoi.ToString();
                db.SubmitChanges();
                return RedirectToAction("TrangChuQuanLy", "QuanLy");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult ThongTinTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThongTinTaiKhoan(FormCollection f)
        {
            string username = Session["tdn"].ToString();
            TAIKHOAN tk = db.TAIKHOANs.Single(t => t.Username == username);
            var HoTen = f["HoTen"];
            var Email = f["Email"];
            var SDT = f["SDT"];
            var pass = f["Pass"];
            var GioiTinh = f["GioiTinh"];
            if (String.IsNullOrEmpty(HoTen))
            {
                ViewData["Loi1"] = "Vui lòng nhập họ tên!";
            }
            if (String.IsNullOrEmpty(Email))
            {
                ViewData["Loi2"] = "Vui lòng nhập email!";
            }
            if (String.IsNullOrEmpty(SDT))
            {
                ViewData["Loi3"] = "Vui lòng nhập số điện thoại!";
            }
            if (String.IsNullOrEmpty(pass))
            {
                ViewData["Loi4"] = "Vui lòng nhập mật khẩu!";
            }
            if (!tk.Pass.Equals(pass))
            {
                Response.Write("<script>alert('Mật khẩu không đúng !!!')</script>");
            }
            if (tk.Pass.Equals(pass) && !String.IsNullOrEmpty(HoTen) && !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(SDT) && !String.IsNullOrEmpty(pass))
            {
                tk.Ten = HoTen.ToString();
                tk.Email = Email.ToString();
                tk.Gioitinh = GioiTinh.ToString();
                tk.SDT = SDT.ToString();
                db.SubmitChanges();
                Session["User"] = tk;
                return RedirectToAction("SanPhamPartial", "SanPham");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ThongTinTaiKhoanQuanLy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThongTinTaiKhoanQuanLy(FormCollection f)
        {
            string username = Session["tdn"].ToString();
            TAIKHOAN tk = db.TAIKHOANs.Single(t => t.Username == username);
            var HoTen = f["HoTen"];
            var Email = f["Email"];
            var SDT = f["SDT"];
            var pass = f["Pass"];
            var GioiTinh = f["GioiTinh"];
            if (String.IsNullOrEmpty(HoTen))
            {
                ViewData["Loi1"] = "Vui lòng nhập họ tên!";
            }
            if (String.IsNullOrEmpty(Email))
            {
                ViewData["Loi2"] = "Vui lòng nhập email!";
            }
            if (String.IsNullOrEmpty(SDT))
            {
                ViewData["Loi3"] = "Vui lòng nhập số điện thoại!";
            }
            if (String.IsNullOrEmpty(pass))
            {
                ViewData["Loi4"] = "Vui lòng nhập mật khẩu!";
            }
            if (!tk.Pass.Equals(pass))
            {
                Response.Write("<script>alert('Mật khẩu không đúng !!!')</script>");
            }
            if (tk.Pass.Equals(pass) && !String.IsNullOrEmpty(HoTen) && !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(SDT) && !String.IsNullOrEmpty(pass))
            {
                tk.Ten = HoTen.ToString();
                tk.Email = Email.ToString();
                tk.Gioitinh = GioiTinh.ToString();
                tk.SDT = SDT.ToString();
                db.SubmitChanges();
                Session["User"] = tk;
                return RedirectToAction("TrangChuQuanLy", "QuanLy");
            }
            else
            {
                return View();
            }
        }
    }
}