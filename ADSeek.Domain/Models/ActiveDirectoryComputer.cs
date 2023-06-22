using System;
using System.ComponentModel;

namespace ADSeek.Domain.Models
{
    public class ActiveDirectoryComputer
    {
        [DisplayName("Уникальное имя записи")]
        public string DistinguishedName { get; set; }
        
        [DisplayName("Операционная система")]
        public string OperatingSystem { get; set; }
        
        [DisplayName("Версия операционной системы")]
        public string OperatingSystemVersion { get; set; }
        
        [DisplayName("Адрес машины")]
        public string DNSHostName { get; set; }
        
        [DisplayName("Имя компьютера")]
        public string SAMAccountName { get; set; }
        
        [DisplayName("Уникальный идентификатор записи")]
        public Guid ObjectGuid { get; set; }
    }
}