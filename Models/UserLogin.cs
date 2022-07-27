using System.ComponentModel.DataAnnotations;

namespace C300.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter User ID")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}