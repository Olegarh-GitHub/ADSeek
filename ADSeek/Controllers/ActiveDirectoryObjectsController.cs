﻿using System;
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
            _service = ActiveDirectoryController._service;
        }

        // private ActiveDirectoryObjectModel _convert(ActiveDirectoryObject adObj)
        // {
        //     return new ActiveDirectoryObjectModel()
        //     {
        //         Attributes = adObj.Attributes.Select(x => new ActiveDirectoryAttributeModel()
        //         {
        //             Attribute = x.Key,
        //             Value = string.Join(",", x.Value.StringValueArray)
        //         }).ToList()
        //     };
        // }
        //
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var targetOu = $"CN=Users,DC={(string.Join(",DC=", _service.Settings.Host.Split(".")))}";
                
                var objects = await _service.SearchAsync(new LdapRequests.SearchRequest(targetOu));

                var dns = objects.Select(x => (x.DistinguishedName, x.ObjectClass)).ToList();
        
                var o = new ActiveDirectoryObjectsListModel()
                {
                    DistinguishedNames = dns
                };
             
                return View("ActiveDirectoryObjectsList", o);
            }
            catch (Exception e)
            {
                return View("/Views/Home/Error_View.cshtml", e);
            }
        }
    }
}