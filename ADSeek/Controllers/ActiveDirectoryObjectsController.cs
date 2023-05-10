using System;
using System.Linq;
using System.Threading.Tasks;
using ADSeek.Application.Interfaces.Services;
using ADSeek.Application.Requests;
using ADSeek.Domain.Models;
using ADSeek.Models;
using Microsoft.AspNetCore.Mvc;

namespace ADSeek.Controllers
{
    public class ActiveDirectoryObjectsController : Controller
    {
        private readonly IActiveDirectoryService _service;

        public ActiveDirectoryObjectsController()
        {
            ViewBag.IsAuthorized = true;
            ViewBag.Account = ActiveDirectoryController._me.DistinguishedName;
            _service = ActiveDirectoryController._service;
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
        public async Task<IActionResult> Index()
        {
            try
            {
                var objects = await _service.SearchAsync(new LdapRequests.SearchRequest("CN=Users,DC=dc,DC=sharipov-bulat,DC=ru"));
                var models = objects.Select(_convert).ToList();

                var dns_raw = models.SelectMany(x => x.Attributes).ToList();
                var dns = dns_raw.Where(x => x.Attribute == "distinguishedName").Select(x => x.Value).ToList();

                var o = new ActiveDirectoryObjectsListModel()
                {
                    DistinguishedNames = dns
                };
                
                ViewBag.IsAuthorized = true;
                ViewBag.Account = ActiveDirectoryController._me.DistinguishedName;
                
                return View("ActiveDirectoryObjectsList", o);
            }
            catch (Exception e)
            {
                return View("/Views/Home/Error_View.cshtml", e);
            }
        }
    }
}