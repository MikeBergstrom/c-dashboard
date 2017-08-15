using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using dashboard.Models;
 
namespace dashboard.Models
{
    public class Lender : BaseEntity
    {
        public int LenderId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime Updatedat {get; set;}
        public int Money {get;set;}
        public List<Transaction> Transactions {get; set;}

        public Lender(){
            Transactions= new List<Transaction>();
        }

    }
}