using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using XSLTransformations.Models;

namespace XSLTransformations.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Navigation()
        {
            List<NavigationLink> links = CreateObject(7);
            string xml = Helper.SerializeToString(links);
            object navigationString = Helper.ApplyXSLT(xml, Server.MapPath("~/Assets/navigation.xslt"));

            return View("Index", navigationString);
        }
        
        public ActionResult NavigationWithCSharpCode()
        {
            List<NavigationLink> links = CreateObject(5);
            string xml = Helper.SerializeToString(links);
            object navigationString = Helper.ApplyXSLT(xml, Server.MapPath("~/Assets/navigation_with_code.xslt"));

            return View("Index", navigationString);
        }

        public ActionResult NavigationWithSaxon()
        {
            List<NavigationLink> links = CreateObject(2);
            string xml = Helper.SerializeToString(links);
            object navigationString = Helper.TransformUsingSaxon(xml, Server.MapPath("~/Assets/navigation.xslt"));

            return View("Index", navigationString);
        } 

        private List<NavigationLink> CreateObject(int levels)
        {
            if (levels > 0)
            {
                //Thread.Sleep(5);
                Random rnd = new Random();
                int children = rnd.Next(1, 10);
                List<NavigationLink> links = new List<NavigationLink>();

                for (int i = 0; i < children; i++)
                {
                    NavigationLink link = new NavigationLink();
                    link.Text = Helper.RandomString(5) + "(" + (levels - 1) + ")";
                    link.Url = "http://www.google.com";
                    link.NavigationLinks = CreateObject(levels - 1);
                    links.Add(link);
                }
                return links;
            }
            return null;
        }
    }
}