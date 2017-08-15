using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using dashboard.Models;
 
namespace dashboard.Models
{
    public class Transaction : BaseEntity
    {
        public int TransactionId {get; set;}
        public int LenderId {get; set;}
        public Lender Lender {get;set;}
        public int BorrowerId {get; set;}
        public Borrower Borrower {get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get; set;}
        public int Amount {get; set;}
    }
}