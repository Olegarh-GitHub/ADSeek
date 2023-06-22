using System;
using System.ComponentModel;

namespace ADSeek.Domain.Models
{
    public class ActiveDirectoryGroup
    {
        [DisplayName("Уникальное имя записи")]
        public string DistinguishedName { get; set; }
        
        [DisplayName("Имя группы")]
        public string SAMAccountName { get; set; }
        
        [DisplayName("Члены группы")]
        public string[] Members { get; set; }
        
        [DisplayName("Уникальный идентификатор записи")]
        public Guid ObjectGuid { get; set; }
    }
}