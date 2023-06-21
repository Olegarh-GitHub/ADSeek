using System.Collections.Generic;
using System.Linq;
using ADSeek.Domain.Constants;
using ADSeek.Domain.Enums;
using ADSeek.Domain.Extensions;
using Novell.Directory.Ldap;

namespace ADSeek.Domain.Models
{
    public class ActiveDirectoryObject
    {
        public ActiveDirectoryObject(LdapAttributeSet attributes)
        {
            Attributes = attributes;
        }

        public string DistinguishedName => Attributes.FirstOrDefault
        (
            attribute => string.Equals(attribute.Key, ActiveDirectoryAttributes.DN_ATTRIBUTE)
        ).Value?.StringValue;
        public LdapAttributeSet Attributes { get; set; }

        public ObjectClass ObjectClass => Attributes
            .FirstOrDefault(item => item.Key == "userAccountControl").Value.ToObjectClass() ?? ObjectClass.Top;
    }
}