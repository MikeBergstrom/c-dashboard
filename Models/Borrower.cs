using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using dashboard.Models;
 
namespace dashboard.Models
{
    public class Borrower : BaseEntity
    {
        public int BorrowerId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        public int Request {get; set;}
        public int Received {get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get; set;}
        public List<Transaction> Transactions {get; set;}

        public Borrower(){
            Transactions= new List<Transaction>();
        }

    }
}