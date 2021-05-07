using System;
using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected Guid ClienteId = Guid.Parse("544c109d-6096-457f-af08-adfd8e523f83");
    }
}