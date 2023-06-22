using Novell.Directory.Ldap;

namespace ADSeek.Application.Requests.Base
{
    public class LdapBaseRequests
    {
        public interface IAttributeSetRequest
        {
            public LdapAttributeSet Attributes { get; set; }
        }
        
        public interface IDistinguishedNameRequest
        {
            public string DistinguishedName { get; set; }
        }
        
        public interface IMoveRequest
        {
            public string TargetDistinguishedName { get; set; }
            public string RelativeDistinguishedName { get; set; }
            public string ParentTargetDistinguishedName { get; set; }
            public bool DeleteOld { get; set; }
        }
    }
}