using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.MVC3.Services;
using UtahPlanners.MVC3.Models.User;
using UtahPlanners.MVC3.Extensions;
using UtahPlanners.MVC3.UserService;

namespace UtahPlanners.MVC3.Controllers
{
    public class UserController : Controller
    {
        private static readonly List<SelectListItem> _themes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Default", Text = "Default" },
            new SelectListItem { Value = "Orange", Text = "Orange" },
            new SelectListItem { Value = "Red", Text = "Red" },
            new SelectListItem { Value = "Blue", Text = "Blue" },
            new SelectListItem { Value = "Green", Text = "Green" }
        };

        private IServiceFactory _factory;

        public UserController(IServiceFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            var client = _factory.CreateUserService();

            if (ModelState.IsValid)
            {
                if (client.SafeExecution(c => c.ValidateUser(model.Username, model.Password)))
                {
                    var authService = _factory.CreateFormsAuthenticationService();
                    authService.SignIn(model.Username, true);
                    return RedirectToAction("Default", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and password combination.");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(CreateAccount model)
        {
            var client = _factory.CreateUserService();
            
            var request = new CreateUserRequest
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                SecurityQuestion = model.SecurityQuestion,
                SecurityAnswer = model.SecurityAnswer
            };
            var status = client.SafeExecution(c => c.CreateUser(request));

            if (status == MembershipStatus.Success)
            {
                var authService = _factory.CreateFormsAuthenticationService();
                authService.SignIn(model.Username, true);
                return RedirectToAction("Default", "Home");
            }

            // TODO: Validation Errors
            return View();
        }

        public ActionResult ForgotPassword(ForgotPassword model)
        {
            if (String.IsNullOrEmpty(model.SecurityAnswer)
                && !String.IsNullOrEmpty(model.Username))
            {
                var client = _factory.CreateUserService();
                var user = client.SafeExecution(c => c.GetUser(model.Username));
                model = new ForgotPassword
                {
                    Username = model.Username,
                    Email = user.Email,
                    SecurityQuestion = user.SecurityQuestion
                };
            }
            
            if (!String.IsNullOrEmpty(model.Username)
                && !String.IsNullOrEmpty(model.Email)
                && !String.IsNullOrEmpty(model.SecurityAnswer))
            {
                var client = _factory.CreateUserService();
                bool result = client.SafeExecution(c => c.ResetPassword(model.Username, model.Email, model.SecurityAnswer));
                model = new ForgotPassword
                {
                    ResetSuccessful = result,
                    Username = model.Username,
                    Email = model.Email,
                    SecurityQuestion = model.SecurityQuestion
                };
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword model)
        {
            var client = _factory.CreateUserService();
            bool result = client.SafeExecution(c => c.ChangePassword(model.Username, model.OldPassword, model.ConfirmPassword));
            model = new ChangePassword { ChangePasswordResult = result };
            return PartialView("_ChangePassword", model);
        }

        [Authorize]
        public ActionResult Profile()
        {
            var client = _factory.CreateUserService();
            User user = client.SafeExecution(c => c.GetUser(User.Identity.Name));
            var profile = new Profile
            {
                Username = user.Username,
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
                Email = user.Email,
                Role = user.Role,
                Theme = user.UserProfile.Theme,
                Themes = new SelectList(_themes, "Value", "Text"),
                ChangePassword = new ChangePassword { Username = user.Username }
            };
            return View(profile);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Profile(Profile model)
        {
            var client = _factory.CreateUserService();

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                UserProfile = new UserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Theme = model.Theme
                }
            };
            var result = client.SafeExecution(c => c.UpdateUser(user));

            model.Themes = new SelectList(_themes, "Value", "Text");
            model.ChangePassword = new ChangePassword { Username = model.Username };
            return View(model);
        }

        public ActionResult Logout()
        {
            var formsAuthService = _factory.CreateFormsAuthenticationService();
            formsAuthService.SignOut();
            return RedirectToAction("Default", "Home");
        }
    }
}
