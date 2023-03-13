using System.ComponentModel.DataAnnotations;

namespace ADSeek.Models
{
    public class ActiveDirectoryAttributeModel
    {
        [Display(Name = "Атрибут")]
        public string Attribute { get; set; }
        
        [Display(Name = "Значение")]
        public string Value { get; set; }
    }
}