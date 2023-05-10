using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADSeek.Models
{
    public class ActiveDirectoryObjectModel
    {
        [Display(Name = "Атрибуты")]
        public List<ActiveDirectoryAttributeModel> Attributes { get; set; }

        public string DistinguishedName => Attributes.FirstOrDefault(x => x.Attribute == "distinguishedName")?.Value;
    }
}