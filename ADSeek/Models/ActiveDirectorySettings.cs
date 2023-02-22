using Microsoft.Extensions.Configuration;

namespace ADSeek.Models
{
    public class ActiveDirectorySettings
    {
        public class ActiveDirectoryConnectionSettings
        {
            public string Host { get; set; }
            public ushort Port { get; set; }
            public ushort SSLPort { get; set; }
            public bool SSLEnabled { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
        
        public ActiveDirectoryConnectionSettings ConnectionSettings { get; set; }

        public ActiveDirectorySettings(IConfiguration configuration)
        {
            configuration.Bind("ActiveDirectorySettings", this);
        }
    }
}