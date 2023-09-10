using System.ComponentModel.DataAnnotations;

namespace Displaying_AccidentProneAreasOnMap.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",
        ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Password are not same")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
