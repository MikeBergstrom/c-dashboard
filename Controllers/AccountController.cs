using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dashboard.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Controllers
{
    public class AccountController : Controller
    {
        private UserContext _context;

        public AccountController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("LenDash")]
        public IActionResult LenDash(){
            int? lenId = HttpContext.Session.GetInt32("LenderId");
            @ViewBag.error = TempData["error"];
            if(lenId ==null){
                return RedirectToAction("login", "User");
            } else {
                ViewBag.lender = _context.Lender.Where(l => l.LenderId == lenId).SingleOrDefault();
                ViewBag.borrowers = _context.Borrower.ToList();
                ViewBag.transactions = _context.Transaction
                                        .Where(t => t.LenderId == lenId)
                                        .Include(t => t.Lender)
                                        .Include(t => t.Borrower)
                                        .ToList();
                return View();
            }
        }

        [HttpGet]
        [Route("BorrDash")]
        public IActionResult BorrDash(){
            int? borrId = HttpContext.Session.GetInt32("BorrowerId");
            if(borrId == null){
                return RedirectToAction("login", "User");
            } else {
                ViewBag.borrower = _context.Borrower.Where(b => b.BorrowerId == borrId).SingleOrDefault();
                ViewBag.transactions = _context.Transaction
                                        .Where(t => t.BorrowerId == borrId)
                                        .Include(t => t.Lender)
                                        .Include(t => t.Borrower)
                                        .ToList();
                return View();
            }
        }

        [HttpPost]
        [Route("Lend")]
        public IActionResult Lend(int Amount, int BorrowerId){
            int lenId = (int)HttpContext.Session.GetInt32("LenderId");
            Lender lender = _context.Lender.Where(l => l.LenderId == lenId).SingleOrDefault();
            if(lender.Money<Amount){
               TempData["error"]  = "Balance is not high enough to lend that amount";
               return RedirectToAction("LenDash");

            }
            Transaction fTran = _context.Transaction.Where(t => t.LenderId == lenId && t.BorrowerId == BorrowerId).SingleOrDefault();
            lender.Money -= Amount;
            Borrower borrower = _context.Borrower.Where(b => b.BorrowerId == BorrowerId).SingleOrDefault();
            borrower.Received += Amount;
            if(fTran == null){
                Transaction transaction = new Transaction();
                transaction.BorrowerId = BorrowerId;
                transaction.LenderId = lenId;
                transaction.CreatedAt = DateTime.Now;
                transaction.UpdatedAt = DateTime.Now;
                transaction.Amount = Amount;
                _context.Add(transaction);
            } else { 
                fTran.Amount += Amount;
                fTran.UpdatedAt = DateTime.Now;
            }
            _context.SaveChanges();
            return RedirectToAction ("LenDash");
        }
    }
}
