using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSeek.Models;
using Novell.Directory.Ldap;

namespace ADSeek.Services
{
    public class ActiveDirectoryService
    {
        private readonly ActiveDirectorySettings _settings;

        public ActiveDirectoryService(ActiveDirectorySettings settings)
        {
            _settings = settings;
        }

        protected async Task<ILdapConnection> ConnectAsync()
        {
            var connection = new LdapConnection();
            var searchConstraints = new LdapSearchConstraints
            {
                ReferralFollowing = true
            };

            connection.Constraints = searchConstraints;

            if (_settings.ConnectionSettings.SSLEnabled)
            {
                connection.SecureSocketLayer = true;
                connection.UserDefinedServerCertValidationDelegate += 
                (
                    sender, 
                    certificate,
                    chain,
                    sslPolicyErrors
                ) => true;
            }

            await connection.ConnectAsync
            (
                _settings.ConnectionSettings.Host,
                _settings.ConnectionSettings.SSLEnabled 
                    ? _settings.ConnectionSettings.SSLPort 
                    : _settings.ConnectionSettings.Port
            );
            await connection.BindAsync
            (
                _settings.ConnectionSettings.Username, 
                _settings.ConnectionSettings.Password
            );

            return connection;
        }

        public virtual async Task<IEnumerable<LdapEntry>> SearchAsync(string lookupDn, string filter = null, string[] attributesToFind = null)
        {
            using var connection = await ConnectAsync();

            var searchRequest = await connection.SearchAsync
            (
                lookupDn,
                LdapConnection.ScopeSub, 
                filter,
                attributesToFind,
                false
            );

            var searchResults = await searchRequest.ToListAsync();
            
            return searchResults;
        }
        
        public virtual async Task AddEntryAsync(string dn, LdapAttributeSet attributeSets)
        {
            using var connection = await ConnectAsync();
            
            var addRequest = new LdapEntry(dn, attributeSets);

            await connection.AddAsync(addRequest);
        }
    }
}