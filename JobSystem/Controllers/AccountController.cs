using System.Security.Claims;
using JobSystem.Entities;
using JobSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CandidateRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CandidateRegistration(CandidateRegViewModel model)
        {
            if (ModelState.IsValid)
            {
                CandidateAccount account = new CandidateAccount();
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Email = model.Email;
                account.Phone = model.Phone;
                account.Password = model.Password;
                account.Profession = model.Profession;
                account.YearsofExperience = model.YearsofExperience;
                account.ProfessionSummary = model.ProfessionSummary;

                try
                {
                    _context.CandidateAccounts.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please Login.";
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Please enter unique Email");
                    return View(model);
                }
                return View();
            }
            return View(model);
        }
        public IActionResult CompanyRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CompanyRegistration(CompanyRegViewModel model)
        {
            if(ModelState.IsValid)
            {
                CompanyAccount account = new CompanyAccount();
                account.CompanyName = model.CompanyName;
                account.Industry = model.Industry;
                account.Email = model.Email;
                account.Phone = model.Phone;
                account.Password = model.Password;
                account.CompanyWebsite = model.CompanyWebsite;
                account.CompanyDescription = model.CompanyDescription;

                try
                {
                    _context.CompanyAccounts.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.CompanyName} registered successfully. Please Login.";
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Please enter unique Email");
                    return View(model);
                }
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var candidate = _context.CandidateAccounts.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                var company = _context.CompanyAccounts.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                if (candidate != null)
                {
                    //Success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, candidate.Email),
                        new Claim("Name", candidate.FirstName),
                        new Claim(ClaimTypes.Role, "Candidate")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("CandidateDashPage");
                }
                else if (company != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, company.Email),
                        new Claim("Name", company.CompanyName),
                        new Claim(ClaimTypes.Role, "Company")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("CompanyDashPage");
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email or Password is incorrect");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult CandidateDashPage()
        {
			ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

        [Authorize]
        public IActionResult CompanyDashPage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
