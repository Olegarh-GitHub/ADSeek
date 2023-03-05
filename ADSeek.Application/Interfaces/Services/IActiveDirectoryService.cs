using System.Collections.Generic;
using System.Threading.Tasks;
using ADSeek.Application.Requests;
using ADSeek.Domain.Models;
using Novell.Directory.Ldap;

namespace ADSeek.Application.Interfaces.Services
{
    public interface IActiveDirectoryService
    {
        public Task<ActiveDirectoryResult> InsertAsync(LdapRequests.CreateRequest request);
        public Task<ActiveDirectoryResult> ModifyAsync(LdapRequests.ModifyRequest request);
        public ActiveDirectoryResult Move(LdapRequests.MoveRequest request);
        public ActiveDirectoryResult Remove(LdapRequests.RemoveRequest request);
        public List<ActiveDirectoryObject> Search(LdapRequests.SearchRequest request);
    }
}