using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CherwellTriangles.Models;

using Newtonsoft.Json;

namespace CherwellTriangles.Controllers
{
    public class HomeController : Controller
    {
        public string TriangleJson
        {
            get
            {
                if (Session == null || Session["Triangles"] == null)
                    return null;
                return Session["Triangles"].ToString();
            }
            set
            {
                Session["Triangles"] = value;
            }
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Cherwell Triangles Coding Challenge";

            var container = ContainerSetup();

            ViewBag.Lookups = new List<string> {
                container.LookupTriangle(0, 10, 0, 0, 10, 10),
                container.LookupTriangle(20, 20, 20, 30, 10, 20),
                container.LookupTriangle(20, 20, 20, 10, 30, 20),
                container.LookupTriangle(40, 30, 40, 40, 30, 30)
            };
            return View(container);
        }

        public ActionResult LookupTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var container = ContainerSetup();
            var message = container.LookupTriangle(x1, y1, x2, y2, x3, y3);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        
        private TriangleContainer ContainerSetup()
        {
            var container = new TriangleContainer(60, 60, 10, 10);
            var json = new JavaScriptSerializer();
            if (string.IsNullOrEmpty(TriangleJson))
            {
                container.CreateTriangles();
            }
            else
            {
                container = json.Deserialize<TriangleContainer>(TriangleJson);
            }

            TriangleJson = json.Serialize(container);
            return container;
        }
    }
}
