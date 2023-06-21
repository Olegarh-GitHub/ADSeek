using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Novell.Directory.Ldap;

namespace ADSeek.Models
{
    public class ActiveDirectoryObjectModel
    {
        [Display(Name = "Атрибуты")]
        public LdapAttributeSet Attributes { get; set; }

        public string DistinguishedName => Attributes.FirstOrDefault(x => x.Key == "distinguishedName").Value.StringValue;
    }
}