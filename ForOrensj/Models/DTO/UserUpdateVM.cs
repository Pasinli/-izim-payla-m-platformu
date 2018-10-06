using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForOrensj.Models
{
    public class UserUpdateVM
    {
        [Required(ErrorMessage ="Lütfen Nick giriniz")]
        public string Nick { get; set; }
        

        public string NameSurname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Mail adresinin doğru girilmesi önemlidir.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Şifre gereklidir.")]
        public string Password { get; set; }

        public string Hakkimda { get; set; }

        public string Phone { get; set; }
    }
}