using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSeek.Application.Interfaces.Services;
using ADSeek.Application.Requests;
using ADSeek.Domain.Models;
using ADSeek.Infrastructure.Mappers;
using ADSeek.Infrastructure.Services;
using ADSeek.Models;
using Microsoft.AspNetCore.Mvc;
using Novell.Directory.Ldap;
using ActiveDirectoryObject = ADSeek.Domain.Models.ActiveDirectoryObject;

namespace ADSeek.Controllers
{
    public class ActiveDirectoryController : Controller
    {
        public static IActiveDirectoryService _service;
        private readonly IServiceProvider _provider;

        public ActiveDirectoryController(IServiceProvider provider)
        {
            _provider = provider;
        }


        private ActiveDirectorySettings.ActiveDirectoryConnectionSettings _settings(string domain, string dn, string password)
        {
            return new ActiveDirectorySettings.ActiveDirectoryConnectionSettings()
            {
                Host = domain,
                Password = password,
                Port = 389,
                SSLEnabled = false,
                SSLPort = 636,
                Username = dn
            };
        }
        
        [HttpPost]
        public async Task<IActionResult> Authorize(ActiveDirectoryAuthorizationModel data)
        {
            string domain = data.Domain;
            
            string dn = data.DistinguishedName;
            string password = data.Password;

            LdapRequests.AuthorizeRequest request = new LdapRequests.AuthorizeRequest(dn, password);

            _service = new ActiveDirectoryService(_settings(domain, dn, password), new ActiveDirectoryConverter());

            ActiveDirectoryResult result = await _service.AuthorizeAsync(request);

            if (!result.IsOk)
            {
                ViewBag.ExceptionByAuthorization = result.ErrorMessage;

                return View("/Views/Home/Index.cshtml");
            }

            ViewBag.IsAuthorized = true;
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ActiveDirectoryObject ad_object = await _service.GetMeAsync();

            ActiveDirectoryObjectModel ad_object_model = _convert(ad_object);

            ViewBag.IsAuthorized = true;
            
            return View("ActiveDirectoryObject", ad_object_model);
        }

        private ActiveDirectoryObjectModel _convert(ActiveDirectoryObject adObj)
        {
            return new ActiveDirectoryObjectModel()
            {
                Attributes = adObj.Attributes.Select(x => new ActiveDirectoryAttributeModel()
                {
                    Attribute = x.Key,
                    Value = string.Join(",", x.Value.StringValueArray)
                }).ToList()
            };
        }
        
        [HttpGet]
        public async Task<IActionResult> Domain_Object(string dn)
        {
            List<ActiveDirectoryObject> ad_objects = await _service.SearchAsync(new LdapRequests.SearchRequest(dn));

            try
            {
                ActiveDirectoryObject ad_object = ad_objects.FirstOrDefault();

                if (ad_object is null)
                {
                    throw new Exception($"Объект с distinguishedName={dn} не найден");
                }

                ActiveDirectoryObjectModel model = _convert(ad_object);
                
                ViewBag.IsAuthorized = true;

                return View("ActiveDirectoryObject", model);
            }
            catch (Exception e)
            {
                return View("/Views/Home/Error_View.cshtml", e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add_Object()
        {
            ViewBag.IsAuthorized = true;
            
            return View("Add_Object");
        }

        [HttpPost]
        public async Task<IActionResult> Add_Object(ActiveDirectoryAddObjectModel model)
        {
            var cn = model.CommonName;
            var dn = $"CN={cn},CN=Users,DC=OLEG,DC=local";

            var attributes = new LdapAttributeSet()
            {
                new("objectClass", "user"),
                new("displayName", model.DisplayName),
                new("givenName", model.GivenName),
                new("sn", model.Surname),
                new("cn", model.CommonName),
                new("sAMAccountName", model.SAMAccountName),
                new("mail", model.Mail)
            };

            var request = new LdapRequests.CreateRequest(dn, attributes);

            var result = await _service.InsertAsync(request);

            if (result.IsOk)
            {
                return RedirectToAction("Domain_Object", new {dn = dn});
            }
            else
            {
                ViewBag.ExceptionByCreation = result.ErrorMessage;
                
                return View("Add_Object");
            }
        }
    }
}