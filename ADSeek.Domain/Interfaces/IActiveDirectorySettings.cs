namespace ADSeek.Domain.Interfaces
{
    public interface IActiveDirectorySettings
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public ushort SSLPort { get; set; }
        public bool SSLEnabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}