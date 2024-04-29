using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide valid Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[RegularExpression(@"^[\w+\W\d]{8,}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
