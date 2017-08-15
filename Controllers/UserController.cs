using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dashboard.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace dashboard.Controllers
{
    public class UserController : Controller
    {
        private UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Register");
        }
        [HttpGet]
        [Route("Register")]
        public IActionResult RegView(){
            return View("Register");
        }
        [HttpPost]
        [Route("RegLender")]
        public IActionResult RegLender(RegisterViewModel user){
            if(ModelState.IsValid){
                List<Lender> lender = _context.Lender.Where(l => l.Email == user.Email).ToList();
                List<Borrower> borrower = _context.Borrower.Where(b => b.Email == user.Email).ToList();
                if(lender.Count >0 || borrower.Count > 0){
                    ViewBag.lendererror = "Email address is already registered";
                    return View("Register");
                } else {
                    PasswordHasher<Lender> Hasher = new PasswordHasher<Lender>();
                    Lender newLender = new Lender {FirstName = user.FirstName, LastName= user.LastName, Email = user.Email, Money = user.Money, CreatedAt = DateTime.Now, Updatedat = DateTime.Now};
                    newLender.Password = Hasher.HashPassword(newLender, user.Password);
                    _context.Add(newLender);
                    _context.SaveChanges();
                    Lender logLender = _context.Lender.SingleOrDefault(u => u.Email == user.Email);
                    HttpContext.Session.SetInt32("LenderId", logLender.LenderId);
                    return RedirectToAction("LenDash", "Account");
                }
            } else {
                return View("Register");
            }
        }
        [HttpPost]
        [Route("RegBorrower")]
        public IActionResult RegBorrower(BorrowerViewModel user){
            if(ModelState.IsValid){
                List<Lender> lender = _context.Lender.Where(l => l.Email == user.Email).ToList();
                List<Borrower> borrower = _context.Borrower.Where(b => b.Email == user.Email).ToList();
                if(lender.Count >0 || borrower.Count > 0){
                    ViewBag.borrowererror = "Email address is already registered";
                    return View("Register");
                } else {
                    PasswordHasher<Borrower> Hasher = new PasswordHasher<Borrower>();
                    Borrower newUser = new Borrower {FirstName = user.FirstName, LastName= user.LastName, Email = user.Email, Title = user.Title, Description = user.Description, Request = user.Money, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now};
                    newUser.Password = Hasher.HashPassword(newUser, user.Password);
                    _context.Add(newUser);
                    _context.SaveChanges();
                    Borrower logUser = _context.Borrower.SingleOrDefault(u => u.Email == user.Email);
                    HttpContext.Session.SetInt32("BorrowerId", logUser.BorrowerId);
                    return RedirectToAction("BorrDash", "Account");
                }
            } else {
                return View("Register");
            }
        }

        [HttpGet]
        [Route("login")]
        public IActionResult GetLogin()
        {
            return View("Login");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string PasswordToCheck)
        {
            Borrower borrower = _context.Borrower.SingleOrDefault(u => u.Email == Email);
            Lender lender = _context.Lender.SingleOrDefault(u => u.Email == Email);
            if(borrower != null && PasswordToCheck != null){
                var Hasher = new PasswordHasher<Borrower>();
                if(0 != Hasher.VerifyHashedPassword(borrower, borrower.Password, PasswordToCheck)){
                    HttpContext.Session.SetInt32("BorrowerId", borrower.BorrowerId);
                    return RedirectToAction("BorrDash", "Account");
                }
            } else if (lender != null && PasswordToCheck != null){
                    var Hasher = new PasswordHasher<Lender>();
                    if(0 != Hasher.VerifyHashedPassword(lender, lender.Password, PasswordToCheck)){
                    HttpContext.Session.SetInt32("LenderId", lender.LenderId);
                    return RedirectToAction("LenDash", "Account");
                }
            }
            ViewBag.error = "Email address of password Incorrect";
            return View("Login");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
