using System.Collections.Generic;
using System.Threading.Tasks;
using ADSeek.Application.Requests;
using ADSeek.Domain.Interfaces;
using ADSeek.Domain.Models;
using Novell.Directory.Ldap;

namespace ADSeek.Application.Interfaces.Services
{
    public interface IActiveDirectoryService
    {
        public IActiveDirectorySettings Settings { get; }
        public Task<ActiveDirectoryResult> AuthorizeAsync(LdapRequests.AuthorizeRequest request);
        public Task<ActiveDirectoryResult> InsertAsync(LdapRequests.CreateRequest request);
        public Task<ActiveDirectoryResult> ModifyAsync(LdapRequests.ModifyRequest request);
        public Task<ActiveDirectoryResult> Move(LdapRequests.MoveRequest request);
        public Task<ActiveDirectoryResult> Remove(LdapRequests.RemoveRequest request);
        public Task<List<ActiveDirectoryObject>> SearchAsync(LdapRequests.SearchRequest request);
        public Task<ActiveDirectoryUser> GetMeAsync();
    }
}