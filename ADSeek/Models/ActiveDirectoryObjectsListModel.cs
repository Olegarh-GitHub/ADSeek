using System.Collections.Generic;
using ADSeek.Domain.Enums;

namespace ADSeek.Models
{
    public class ActiveDirectoryObjectsListModel
    {
        public List<(string dn, ObjectClass objectClass)> DistinguishedNames { get; set; }
    }
}