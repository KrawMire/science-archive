using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScienceArchive.Api.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Update()
        {
            return View();
        }
    }
}

