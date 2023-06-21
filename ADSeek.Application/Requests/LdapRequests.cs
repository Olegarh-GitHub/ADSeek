using ADSeek.Application.Interfaces.Services;
using ADSeek.Application.Requests.Base;
using Novell.Directory.Ldap;

namespace ADSeek.Application.Requests
{
    public class LdapRequests
    {
        public class AuthorizeRequest : LdapBaseRequests.IDistinguishedNameRequest
        {
            public string Password { get; set; }
            
            public AuthorizeRequest(string distinguishedName, string password)
            {
                DistinguishedName = distinguishedName;
                Password = password;
            }

            public string DistinguishedName { get; set; }
        }

        public class CreateRequest : LdapBaseRequests.IAttributeSetRequest, LdapBaseRequests.IDistinguishedNameRequest
        {
            public string DistinguishedName { get; set; }
            public LdapAttributeSet Attributes { get; set; }
            
            public CreateRequest(string dn, LdapAttributeSet attributes)
            {
                DistinguishedName = dn;
                Attributes = attributes;
            }
        }
        
        public class ModifyRequest : LdapBaseRequests.IAttributeSetRequest, LdapBaseRequests.IDistinguishedNameRequest
        {
            public LdapAttributeSet Attributes { get; set; }
            public string DistinguishedName { get; set; }

            public ModifyRequest(string dn, LdapAttributeSet attributes)
            {
                DistinguishedName = dn;
                Attributes = attributes;
            }
        }
        
        public class RemoveRequest : LdapBaseRequests.IDistinguishedNameRequest
        {
            public string DistinguishedName { get; set; }
        }

        public class MoveRequest : LdapBaseRequests.IMoveRequest
        {
            public MoveRequest(string relativeDistinguishedName, string parentTargetDistinguishedName, bool deleteOld)
            {
                RelativeDistinguishedName = relativeDistinguishedName;
                ParentTargetDistinguishedName = parentTargetDistinguishedName;
                DeleteOld = deleteOld;
            }
            public string TargetDistinguishedName { get; set; }
            public string RelativeDistinguishedName { get; set; }
            public string ParentTargetDistinguishedName { get; set; }
            public bool DeleteOld { get; set; } = true;
        }
        
        public class SearchRequest : LdapBaseRequests.IDistinguishedNameRequest
        {
            public SearchRequest(string distinguishedName, string filter = null)
            {
                DistinguishedName = distinguishedName;
                Filter = filter;
            }

            public string DistinguishedName { get; set; }
            public string Filter { get; set; }
        }
    }
}