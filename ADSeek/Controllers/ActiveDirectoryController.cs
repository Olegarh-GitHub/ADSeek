using System.Linq;
using System.Threading.Tasks;
using ADSeek.Application.Interfaces.Services;
using ADSeek.Application.Requests;
using ADSeek.Domain.Models;
using ADSeek.Infrastructure.Mappers;
using ADSeek.Infrastructure.Services;
using ADSeek.Models;
using Microsoft.AspNetCore.Mvc;
using ActiveDirectoryObject = ADSeek.Domain.Models.ActiveDirectoryObject;

namespace ADSeek.Controllers
{
    public class ActiveDirectoryController : Controller
    {
        private static IActiveDirectoryService _service;

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

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ActiveDirectoryObject ad_object = await _service.GetMeAsync();

            ActiveDirectoryObjectModel ad_object_model = new ActiveDirectoryObjectModel()
            {
                Attributes = ad_object.Attributes.Select(x => new ActiveDirectoryAttributeModel()
                {
                    Attribute = x.Key,
                    Value = string.Join(",", x.Value.StringValueArray)
                }).ToList()
            };

            return View("ActiveDirectoryObject", ad_object_model);
        }
    }
}