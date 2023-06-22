using System;
using System.ComponentModel;
using ADSeek.Domain.Enums;

namespace ADSeek.Domain.Models
{
    public class ActiveDirectoryUser
    {
        [DisplayName("Уникальный идентификатор объекта")]
        public Guid ObjectGuid { get; set; }
        
        [DisplayName("Уникальное имя записи объекта")]
        public string DistinguishedName { get; set; }
        
        [DisplayName("Отображаемое имя")]
        public string DisplayName { get; set; }
        
        [DisplayName("Имя")]
        public string GivenName { get; set; }
        
        [DisplayName("Имя записи")]
        public string CommonName { get; set; }
        
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        
        [DisplayName("Инициалы")]
        public string Initials { get; set; }
        
        [DisplayName("Фото")]
        public byte[] Photo { get; set; } 
        
        [DisplayName("Адрес эл.почты")]
        public string Mail { get; set; }
        
        [DisplayName("Имя аккаунта")]
        public string SamAccountName { get; set; }
        
        [DisplayName("Опции учетной записи")]
        public UserAccountControl UserAccountControl { get; set; }
        
        public ObjectClass ObjectClass { get; set; }
        
        [DisplayName("Член групп")]
        public string[] MemberOf { get; set; }
    }
}