using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADSeek.Application.Interfaces.Services;
using ADSeek.Application.Requests;
using ADSeek.Domain.Models;
using ADSeek.Infrastructure.Mappers;
using ADSeek.Infrastructure.Services;
using ADSeek.Inputs;
using ADSeek.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Novell.Directory.Ldap;
using ActiveDirectoryObject = ADSeek.Domain.Models.ActiveDirectoryObject;

namespace ADSeek.Controllers
{
    public class ActiveDirectoryController : Controller
    {
        public static IActiveDirectoryService _service;
        private readonly IServiceProvider _provider;
        public static ActiveDirectoryObjectModel _me;
        private readonly IMapper _mapper;

        public ActiveDirectoryController(IServiceProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }


        private ActiveDirectorySettings.ActiveDirectoryConnectionSettings _settings(string domain, string dn, string password)
        {
            return new ActiveDirectorySettings.ActiveDirectoryConnectionSettings()
            {
                Host = domain,
                Password = password,
                Port = 389,
                SSLEnabled = true,
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

            _service = new ActiveDirectoryService(_settings(domain, dn, password), new ActiveDirectoryConverter(), _mapper);

            ActiveDirectoryResult result = await _service.AuthorizeAsync(request);

            if (!result.IsOk)
            {
                ViewBag.ExceptionByAuthorization = result.ErrorMessage;

                return View("/Views/Home/Index.cshtml");
            }

            Session.CURRENT_USER = await _service.GetMeAsync();
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("/Views/ActiveDirectoryUser/ActiveDirectoryUserView.cshtml", Session.CURRENT_USER);
        }

        [HttpGet]
        public async Task<IActionResult> UserView(string dn)
        {
            var neededObject = await _service.SearchAsync(new LdapRequests.SearchRequest(dn));

            var user = _mapper.Map<ActiveDirectoryUser>(neededObject.FirstOrDefault());

            return View("/Views/ActiveDirectoryUser/ActiveDirectoryUserView.cshtml", user);
        }
        
        [HttpGet]
        public async Task<IActionResult> GroupView(string dn)
        {
            var neededObject = await _service.SearchAsync(new LdapRequests.SearchRequest(dn));

            var group = _mapper.Map<ActiveDirectoryGroup>(neededObject.FirstOrDefault());

            return View("/Views/ActiveDirectoryGroup/ActiveDirectoryGroupView.cshtml", group);
        }
        
        [HttpGet]
        public async Task<IActionResult> ComputerView(string dn)
        {
            var neededObject = await _service.SearchAsync(new LdapRequests.SearchRequest(dn));

            var computer = _mapper.Map<ActiveDirectoryComputer>(neededObject.FirstOrDefault());

            return View("/Views/ActiveDirectoryComputer/ActiveDirectoryComputerView.cshtml", computer);
        }
        
        [HttpGet]
        public async Task<IActionResult> ObjectView(string dn)
        {
            var neededObject = await _service.SearchAsync(new LdapRequests.SearchRequest(dn));
            
            return View("/Views/ActiveDirectory/ActiveDirectoryObjectView.cshtml", neededObject.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> UploadUserPhoto(ActiveDirectoryUserPhotoUploadModel input)
        {
            var attributesToModify = new LdapAttributeSet();

            await using var memoryStream = new MemoryStream();
            memoryStream.Seek(0, SeekOrigin.Begin);

            await input.FormFile.CopyToAsync(memoryStream);

            attributesToModify.Add("photo", new("photo", memoryStream.ToArray()));

            var modifyRequest = new LdapRequests.ModifyRequest(input.DistinguishedName, attributesToModify);

            try
            {
                await _service.ModifyAsync(modifyRequest);
            }
            catch (Exception exception)
            {
                return View("/Views/Home/Error_View.cshtml", exception);
            }
            
            return RedirectToAction("UserView", new {dn = input.DistinguishedName});
        }

        [HttpGet]
        public async Task<IActionResult> Add_Object()
        {
            return View("Add_Object");
        }

        [HttpPost]
        public async Task<IActionResult> Add_Object(CreateUserInput model)
        {
            var cn = model.DisplayName;
            var dn = $"CN={cn},CN=Users,DC=OLEG,DC=local";
            dn = $"CN={cn},CN=Users,DC=dc,DC=sharipov-bulat,DC=ru";

            var attributes = new LdapAttributeSet();

            try
            {
                attributes = new LdapAttributeSet()
                {
                    new("objectClass", "user"),
                    new("initials", model.Initials),
                    new("displayName", model.DisplayName),
                    new("givenName", model.GivenName),
                    new("sn", model.Surname),
                    new("cn", cn),
                    new("sAMAccountName", model.AccountName),
                    new("mail", model.Mail)
                };
            }
            catch
            {
                ViewBag.ExceptionByCreation = "Для успешного создания объекта необходимо заполнить все поля";

                return View("Add_Object");
            }

            var request = new LdapRequests.CreateRequest(dn, attributes);

            var result = await _service.InsertAsync(request);
            
            var password = model.Password;

            var attributesToModify = new LdapAttributeSet()
            {
                new("pwdLastSet", "0"),
                new("unicodePwd", Encoding.Unicode.GetBytes($"\"{password}\"")),
                new("userAccountControl", "512")
            };
            
            var modifyRequest = new LdapRequests.ModifyRequest(dn, attributesToModify);

            var result2 = await _service.ModifyAsync(modifyRequest);

            if (model.Photo is not null)
            {       
                await using var memoryStream = new MemoryStream();
                memoryStream.Seek(0, SeekOrigin.Begin);

                await model.Photo.CopyToAsync(memoryStream);

                var photoAttributes = new LdapAttributeSet()
                {

                    new("photo", memoryStream.ToArray())
                };

                var photoRequest = new LdapRequests.ModifyRequest(dn, photoAttributes);

                await _service.ModifyAsync(photoRequest);
            }
            
            if (result.IsOk && result2.IsOk)
            {
                return RedirectToAction("UserView", new {dn = dn});
            }
            else
            {
                ViewBag.ExceptionByCreation = result.ErrorMessage;
                
                return View("Add_Object");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View("ActiveDirectorySearch");
        }

        [HttpPost]
        public async Task<IActionResult> Search(ActiveDirectorySearchModel model)
        {
            try
            {
                string dn = model.DistinguishedName;
                string filter = model.Filter;

                var search_results = await _service.SearchAsync(new LdapRequests.SearchRequest(dn, filter));

                var dns = search_results.Select(item => (item.DistinguishedName, item.ObjectClass)).ToList();

                return View("/Views/ActiveDirectoryObjects/ActiveDirectorySearchResults.cshtml", new ActiveDirectoryObjectsListModel() { DistinguishedNames = dns});
            }
            catch (Exception e)
            {
                return View("/Views/Home/Error_View.cshtml");
            }
        }
    }
}