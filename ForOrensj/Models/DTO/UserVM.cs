using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForOrensj.Models
{
    public class UserVM
    {
        [Required(ErrorMessage ="Nick giriniz")]
        public string Nick { get; set; }
        [EmailAddress(ErrorMessage ="Mail adresinin doğru girilmesi gerek.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Şifre girmeniz gerekli.")]
        public string Password { get; set; }

    }
}