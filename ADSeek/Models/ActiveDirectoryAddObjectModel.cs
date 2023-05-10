using System.ComponentModel.DataAnnotations;

namespace ADSeek.Models
{
    public class ActiveDirectoryAddObjectModel
    {
        [Display(Name = "Отображаемое имя")]
        public string DisplayName { get; set; }
        
        [Display(Name = "Имя")]
        public string GivenName { get; set; }
        
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        
        [Display(Name = "Общее имя")]
        public string CommonName { get; set; }
        
        [Display(Name = "Имя аккаунта")]
        public string SAMAccountName { get; set; }
        
        [Display(Name = "Эл.почта")]
        public string Mail { get; set; }
    }
}