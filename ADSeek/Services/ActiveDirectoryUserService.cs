using System.Threading.Tasks;
using ADSeek.Application.Interfaces.Services;
using ADSeek.Application.Requests;
using ADSeek.Domain.Models;
using ADSeek.Inputs;
using AutoMapper;
using Novell.Directory.Ldap;

namespace ADSeek.Services
{
    public class ActiveDirectoryUserService
    {
        private readonly IActiveDirectoryService _service;
        private readonly IMapper _mapper;

        public ActiveDirectoryUserService(IActiveDirectoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<ActiveDirectoryResult> CreateUser(CreateUserInput input)
        {
            string dn = $"CN={input.DisplayName}{(string.Join(",DC=", _service.Settings.Host.Split(".")))}";

            var attributes = _mapper.Map<LdapAttributeSet>(input);
            
            var request = new LdapRequests.CreateRequest(dn, attributes);

            return await _service.InsertAsync(request);
        }

        public async Task UpdateUser(ActiveDirectoryUser updatedUser)
        {
            
        }

        public async Task RemoveUser(RemoveUserInput input)
        {
            
        }
        
    }
}