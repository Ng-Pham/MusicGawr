using Application.Account;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        // GET: Acconut
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly IResetPassword _resetPassword;

        public AccountController(ILoginService loginService, IRegisterService registerService, IResetPassword resetPassword)
        {
            _loginService = loginService;
            _registerService = registerService;
            _resetPassword = resetPassword;
        }
        public AccountController()
        {
        }

        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var user = _loginService.Login(model);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, model.RememberMe);
                    Session["CurrentUser"] = user;

                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                }

                return Json(new { success = false, message = "Tên đăng nhập hoặc mật khẩu không đúng." });
            }
            catch (Exception ex)
            {
                // Gửi chi tiết lỗi về client để debug (chỉ dùng trong môi trường dev)
                return Json(new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var result = _registerService.Register(model);
                if (result.Success)
                {
                    FormsAuthentication.SetAuthCookie(result.User.Username, false);
                    Session["CurrentUser"] = result;
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                }

                return Json(new { success = false, message = result.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["CurrentUser"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ResetPasswordViewModel model)
        {
            model.Context = "Forgot"; // báo ngữ cảnh là "Forgot"
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Code");

            if (!TryValidateModel(model))
            {
                return Json(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var result = _resetPassword.ForgotPass(model.Email);
            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        public ActionResult ResetPassword(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return View("Error", new { message = "Mã xác nhận không hợp lệ." });
            }

            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            model.Context = "Reset"; // báo ngữ cảnh là "Reset"

            if (!TryValidateModel(model))
            {
                return Json(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var result = _resetPassword.ResetPass(model.Code, model.Password);
            return Json(new { success = result.Success, message = result.Message });
        }
    }
}