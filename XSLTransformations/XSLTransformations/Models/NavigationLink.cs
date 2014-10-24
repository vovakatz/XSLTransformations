using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XSLTransformations.Models
{
    public class NavigationLink
    {
        public NavigationLink() { }

        public string Text { get; set; }
        public string Url { get; set; }
        public List<NavigationLink> NavigationLinks { get; set; }
    }
}