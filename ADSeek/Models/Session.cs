using System.Linq;
using ADSeek.Domain.Models;

namespace ADSeek.Models
{
    public static class Session
    {
        public static ActiveDirectoryUser CURRENT_USER { get; set; }

        public static bool IS_AUTHORIZED => CURRENT_USER is not null;
        public static bool IS_DOMAIN_ADMINISTRATOR => CURRENT_USER?.MemberOf.Any(item => item.Contains("CN=Administrators,CN=Builtin")) ?? false;
    }
}