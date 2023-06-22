using System.ComponentModel;

namespace ADSeek.Inputs
{
    public class CreateUserInput
    {
        [DisplayName("Отображаемое имя")]
        public string DisplayName { get; set; }
        
        [DisplayName("Имя")]
        public string GivenName { get; set; }
        
        [DisplayName("Инициалы")]
        public string Initials { get; set; }
        
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        
        [DisplayName("Включить в группы")]
        public string[] MemberOf { get; set; }
        
        [DisplayName("Адрес эл.почты")]
        public string Mail { get; set; }
        
        [DisplayName("Фотография")]
        public byte[] Photo { get; set; }
        
        [DisplayName("Пароль")]
        public string Password { get; set; }
        
        [DisplayName("Имя аккаунта для входа")]
        public string AccountName { get; set; }
    }
}