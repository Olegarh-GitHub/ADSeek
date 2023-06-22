using System.ComponentModel;

namespace ADSeek.Domain.Models
{
    public class ActiveDirectoryComputer
    {
        [DisplayName("Операционная система")]
        public string OperatingSystem { get; set; }
        
        [DisplayName("Версия операционной системы")]
        public string OperatingSystemVersion { get; set; }
        
        [DisplayName("Адрес машины")]
        public string DNSHostName { get; set; }
        
        [DisplayName("Имя компьютера")]
        public string SAMAccountName { get; set; }
    }
}