using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    public class BaseController: Controller
    {
        protected ApplicationDbContext _ctx;

        public BaseController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

    }
}
