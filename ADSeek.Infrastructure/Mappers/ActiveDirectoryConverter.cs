using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ADSeek.Application.Interfaces.Mappers;
using ADSeek.Domain.Models;
using Novell.Directory.Ldap;

namespace ADSeek.Infrastructure.Mappers
{
    public class ActiveDirectoryConverter : IActiveDirectoryConverter<LdapAttributeSet>
    {
        public ActiveDirectoryObject Map(LdapAttributeSet source)
        {
            return new ActiveDirectoryObject(source);
        }

        public IEnumerable<ActiveDirectoryObject> Map(IEnumerable<LdapAttributeSet> source)
        {
            return source.Select(Map);
        }
    }
}