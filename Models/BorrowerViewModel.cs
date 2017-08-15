using System.ComponentModel.DataAnnotations;
namespace dashboard.Models
{
    public class BorrowerViewModel : BaseEntity
    {
        [Required(ErrorMessage="First Name is Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="First Name not in Proper Format")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage="Last Name is Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="First Name not in Proper Format")]
        [Display(Name="Last Name")]
        public string LastName { get; set; }
 
        [Required(ErrorMessage="Email Address is Required")]
        [EmailAddress]
        [Display(Name="Email Address")]
        public string Email { get; set; }
 
        [Required(ErrorMessage="Password is Required")]
        [MinLength(8)]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        [Display(Name="Confirm Password")]
        public string Confirm { get; set; }

        [Required(ErrorMessage="Dollar Amount is Required")]
        [Display(Name="Dollar Amount")]
        public int Money {get;set;}

        [Required(ErrorMessage="Need Money for is Required")]
        public string Title {get; set;}
        [Required(ErrorMessage="Description is required")]
        public string Description {get; set;}
    }
}