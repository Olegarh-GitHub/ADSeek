using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSeek.Application.Interfaces.Mappers;
using ADSeek.Application.Interfaces.Services;
using ADSeek.Application.Requests;
using ADSeek.Domain.Interfaces;
using ADSeek.Domain.Models;
using AutoMapper;
using Novell.Directory.Ldap;

namespace ADSeek.Infrastructure.Services
{
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private readonly IActiveDirectorySettings _settings;
        private readonly IActiveDirectoryConverter<LdapAttributeSet> _converter;
        private readonly IMapper _mapper;
        
        public ActiveDirectoryService
        (
            IActiveDirectorySettings settings,
            IActiveDirectoryConverter<LdapAttributeSet> converter, IMapper mapper)
        {
            _settings = settings;
            _converter = converter;
            _mapper = mapper;
        }

        private async Task<bool> _isExists(string dn)
        {
            using ILdapConnection connection = await ConnectAsync();

            LdapEntry entry = await connection.ReadAsync(dn);

            return entry != null;
        }

        protected async Task<ILdapConnection> ConnectAsync()
        {
            var connection = new LdapConnection();
            var searchConstraints = new LdapSearchConstraints
            {
                ReferralFollowing = true
            };

            connection.Constraints = searchConstraints;

            if (_settings.SSLEnabled)
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
                _settings.Host,
                _settings.SSLEnabled 
                    ? _settings.SSLPort 
                    : _settings.Port
            );
            
            await connection.BindAsync
            (
                _settings.Username, 
                _settings.Password
            );

            return connection;
        }

        public IActiveDirectorySettings Settings => _settings;

        public async Task<ActiveDirectoryResult> AuthorizeAsync(LdapRequests.AuthorizeRequest request)
        {
            try
            {
                using ILdapConnection connection = await ConnectAsync();

                return new ActiveDirectoryResult();
            }
            catch (Exception e)
            {
                return new ActiveDirectoryResult(exception: e);
            }
        }

        public async Task<ActiveDirectoryResult> InsertAsync(LdapRequests.CreateRequest request)
        {
            string dn = request.DistinguishedName;
            LdapAttributeSet attributes = request.Attributes;

            using ILdapConnection connection = await ConnectAsync();

            LdapEntry entry = new LdapEntry(dn, attributes);

            try
            {
                await connection.AddAsync(entry);

                return new ActiveDirectoryResult();
            }
            catch (Exception exception)
            {
                return new ActiveDirectoryResult(exception);
            }
        }

        public async Task<ActiveDirectoryResult> ModifyAsync(LdapRequests.ModifyRequest request)
        {
            string dn = request.DistinguishedName;

            bool isExists = await _isExists(dn);
            
            if (!isExists)
                return new ActiveDirectoryResult(isOk: false, errorMessage: $"Object with {dn} distinguishedName is not exist");
            
            using var connection = await ConnectAsync();

            LdapAttributeSet attributes = request.Attributes;

            try
            {
                foreach (var attribute in attributes)
                {
                    LdapModification modification = new LdapModification(LdapModification.Replace, attribute);
                
                    await connection.ModifyAsync
                    (
                        dn,
                        modification
                    );
                }

                return new ActiveDirectoryResult();
            }
            catch (Exception exception)
            {
                return new ActiveDirectoryResult(exception);
            }
        }

        public async Task<ActiveDirectoryResult> Move(LdapRequests.MoveRequest request)
        {
            using var connection = await ConnectAsync();

            var dn = request.TargetDistinguishedName;
            var rdn = request.RelativeDistinguishedName;
            var parent = request.ParentTargetDistinguishedName;
            var delete = request.DeleteOld;

            try
            {
                await connection.RenameAsync
                (
                    dn,
                    rdn,
                    parent,
                    delete
                );

                return new ActiveDirectoryResult();
            }
            catch (Exception exception)
            {
                return new ActiveDirectoryResult(exception);
            }
        }

        public async Task<ActiveDirectoryResult> Remove(LdapRequests.RemoveRequest request)
        {
            using var connection = await ConnectAsync();

            try
            {
                await connection.DeleteAsync(request.DistinguishedName);

                return new ActiveDirectoryResult();
            }
            catch (Exception exception)
            {
                return new ActiveDirectoryResult(exception);
            }
        }

        public async Task<List<ActiveDirectoryObject>> SearchAsync(LdapRequests.SearchRequest request)
        {
            using ILdapConnection connection = await ConnectAsync();

            string lookupDn = string.IsNullOrEmpty(request.DistinguishedName) ? string.Empty : request.DistinguishedName;
            string filter = request.Filter;

            ILdapSearchResults result = await connection.SearchAsync
            (
                lookupDn, LdapConnection.ScopeSub, filter, null, false
            );

            List<LdapEntry> entries = await result.ToListAsync();
            
            return _converter.Map(entries.Select(entry => entry.GetAttributeSet())).ToList();
        }

        public async Task<ActiveDirectoryUser> GetMeAsync()
        {
            var me = await SearchAsync(new LdapRequests.SearchRequest(_settings.Username));

            var user = _mapper.Map<ActiveDirectoryUser>(me.FirstOrDefault());
            
            return user;
        }
    }
}