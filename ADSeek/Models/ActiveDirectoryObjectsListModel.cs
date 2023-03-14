using System.Collections.Generic;

namespace ADSeek.Models
{
    public class ActiveDirectoryObjectsListModel
    {
        public List<ActiveDirectoryObjectList> ObjectList { get; set; }

        public class ActiveDirectoryObjectList
        {
            public string DistinguishedName { get; set; }
        }
    }
}