using System.Collections.Generic;
using System.Threading.Tasks;
using ADSeek.Application.Requests;
using ADSeek.Domain.Models;
using Novell.Directory.Ldap;

namespace ADSeek.Application.Interfaces.Services
{
    public interface IActiveDirectoryService
    {
        public Task<ActiveDirectoryResult> AuthorizeAsync(LdapRequests.AuthorizeRequest request);
        public Task<ActiveDirectoryResult> InsertAsync(LdapRequests.CreateRequest request);
        public Task<ActiveDirectoryResult> ModifyAsync(LdapRequests.ModifyRequest request);
        public Task<ActiveDirectoryResult> Move(LdapRequests.MoveRequest request);
        public Task<ActiveDirectoryResult> Remove(LdapRequests.RemoveRequest request);
        public Task<List<ActiveDirectoryObject>> SearchAsync(LdapRequests.SearchRequest request);
        public Task<ActiveDirectoryObject> GetMeAsync();
    }
}