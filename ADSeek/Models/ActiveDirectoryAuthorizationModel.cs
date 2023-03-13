using System.ComponentModel.DataAnnotations;

namespace ADSeek.Models
{
    public class ActiveDirectoryAuthorizationModel
    {
        [Display(Name = "Домен для подключения")]
        public string Domain { get; set; }
        
        [Display(Name = "Учетная запись для подключения")]
        public string DistinguishedName { get; set; }
        
        [Display(Name = "Пароль для подключения")]
        public string Password { get; set; }
    }
}