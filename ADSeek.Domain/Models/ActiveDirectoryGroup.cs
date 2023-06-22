using System;

namespace ADSeek.Domain.Models
{
    public class ActiveDirectoryGroup
    {
        public string DistinguishedName { get; set; }
        public string SAMAccountName { get; set; }
        public string[] Members { get; set; }
        public Guid ObjectGuid { get; set; }
    }
}