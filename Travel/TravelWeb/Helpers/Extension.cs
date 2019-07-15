using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using System.Web.Routing;


namespace TravelWeb.Helpers
{
    public static class Extension
    {
        private static readonly string Path = HttpContext.Current.Server.MapPath("~/Images");

        public static string IsActive(this HtmlHelper html, string control, string action = null)
        {
            var routeData = html.ViewContext.RouteData;
            var routeControl = (string)routeData.Values["controller"];
            var routeAction = (string)routeData.Values["action"];
            var returnActive = action == null ? control == routeControl : control == routeControl && routeAction == action;
            return returnActive ? "active" : "";
        }

        public static string UploadImage(HttpPostedFileBase[] images, string imageListOld = null)
        {
            var listImageName = new List<string>();
            foreach (var image in images)
            {
                var imageName = DateTime.Now.Ticks + image.FileName + ".jpg";
                string path = System.IO.Path.Combine(Path, imageName);
                image.SaveAs(path);
                listImageName.Add(imageName);
            }
            if (imageListOld != null)
                listImageName.AddRange(imageListOld.Split(','));
            return string.Join(",", listImageName);
        }
        public static string DeleteImage(string imageName, List<string> imageList)
        {
            if (!File.Exists(System.IO.Path.Combine(Path, imageName)))
                return string.Join(",", imageList);
            File.Delete(System.IO.Path.Combine(Path, imageName));
            imageList.RemoveAt(imageList.IndexOf(imageName));
            return string.Join(",", imageList);

        }
    }
}