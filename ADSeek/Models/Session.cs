using System.Linq;
using ADSeek.Domain.Models;

namespace ADSeek.Models
{
    public static class Session
    {
        public static ActiveDirectoryObject CURRENT_USER { get; set; }

        public static bool IS_DOMAIN_ADMINISTRATOR = CURRENT_USER?.Attributes
            .Where(attribute => attribute.Key == "memberOf")
            .SelectMany
            (
                attribute => attribute.Value?.StringValueArray
            )
            .ToList()
            .Contains("CN=Administrators,CN=Builtin") ?? false;
    }
}