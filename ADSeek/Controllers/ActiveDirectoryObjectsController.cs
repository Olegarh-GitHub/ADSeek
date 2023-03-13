using System;
using System.Threading.Tasks;
using ADSeek.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADSeek.Controllers
{
    public class ActiveDirectoryObjectsController : Controller
    {
        private readonly IActiveDirectoryService _service;

        public ActiveDirectoryObjectsController(IActiveDirectoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            throw new NotImplementedException();
        }
    }
}