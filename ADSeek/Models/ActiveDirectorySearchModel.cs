using System.ComponentModel.DataAnnotations;

namespace ADSeek.Models
{
    public class ActiveDirectorySearchModel
    {
        [Display(Name = "Уникальный идентификатор записи")]
        public string DistinguishedName { get; set; }
        
        [Display(Name = "Фильтр")]
        public string Filter { get; set; }
    }
}