using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.MVC3.Services;
using UtahPlanners.MVC3.Models.User;
using UtahPlanners.MVC3.Extensions;
using UtahPlanners.MVC3.MembershipService;

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
            var client = _factory.CreateMembershipService();

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
            var client = _factory.CreateMembershipService();
            
            var request = new CreateUserRequest
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                SecurityQuestion = model.SecurityQuestion,
                SecurityAnswer = model.SecurityAnswer
            };
            var status = client.CreateUser(request);

            if (status == MembershipService.MembershipStatus.Success)
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
                var client = _factory.CreateMembershipService();
                var user = client.GetUser(model.Username);
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
                var client = _factory.CreateMembershipService();
                bool result = client.ResetPassword(model.Username, model.Email, model.SecurityAnswer);
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
        public ActionResult Profile()
        {
            var client = _factory.CreateMembershipService();
            User user = client.GetUser(User.Identity.Name);
            var profile = new Profile
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Themes = new SelectList(_themes, "Value", "Text"),
            };
            return View(profile);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Profile(Profile model)
        {
            var client = _factory.CreateMembershipService();
            bool result = client.SafeExecution(c => c.ChangePassword(model.Username, model.OldPassword, model.NewPassword1));

            model = new Profile
            {
                Username = model.Username,
                Themes = new SelectList(_themes, "Value", "Text"),
            };
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
