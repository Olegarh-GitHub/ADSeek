using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADSeek.Models
{
    public class ActiveDirectoryObjectModel
    {
        [Display(Name = "Атрибуты")]
        public List<ActiveDirectoryAttributeModel> Attributes { get; set; }
    }
}