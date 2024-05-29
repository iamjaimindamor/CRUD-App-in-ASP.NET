using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegisterationForm.Models
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Field Cannot Be Empty")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Enter Valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Cannot Be Empty")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password Must Have Minimum 8 Character" +
            "Contains Special Character" +
            "Contains atleast one Uppercase and Lowercase Letter" +
            "Contains atleast single digit number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("Password", ErrorMessage = "Both Password Must be same")]
        [Display(Name = "Confirm Password")]
        [Column("Confirm Password")]
        public string Confirm_Password { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(12, 32)]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression("(?<!\\d)\\d{10}(?!\\d)", ErrorMessage = "Contains 10 digit")]
        public double? Phone_Number { get; set; }
    }
}