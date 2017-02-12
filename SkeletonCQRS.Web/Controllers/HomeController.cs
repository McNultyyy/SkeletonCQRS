using System;
using System.IO;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SkeletonCQRS.Data;
using SkeletonCQRS.Data.Entities;
using SkeletonCQRS.Data.Repositories;
using SkeletonCQRS.Domain.Commands;
using SkeletonCQRS.Domain.Events;
using SkeletonCQRS.Domain.Queries;
using SkeletonCQRS.Infrastructure.Commands;
using SkeletonCQRS.Infrastructure.Queries;

namespace SkeletonCQRS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public HomeController(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        public ActionResult Index()
        {
            var guid = Guid.NewGuid();
            _commandProcessor.Process(new CreateInventoryItem(guid, "created book"));
            _commandProcessor.Process(new RenameInventoryItem(guid, "i am a new book name"));

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}