namespace IRunes.Controllers
{
    using System;
    using System.Linq;
    using System.Net;

    using IRunes.Models;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;

    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest request)
        {
            if (!base.IsAuthenticated(request))
                return new RedirectResult("/Users/Login");

            string albumId = request.QueryData["?albumId"].ToString();
            base.ViewBag["albumId"] = albumId;

            return base.View();
        }

        public IHttpResponse DoCreate(IHttpRequest request)
        {
            string name = request.FormData["name"].ToString();
            string link = request.FormData["link"].ToString();
            decimal price = decimal.Parse(request.FormData["price"].ToString());

            string referer = request.Headers.GetHeader("Referer").ToString();
            int index = referer.IndexOf("=");
            string albumId = referer.Substring(index + 1);

            var track = new Track
            {
                Name = name,
                Link = link,
                Price = price,
                AlbumId = albumId
            };

            DbContext.Tracks.Add(track);

            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HtmlResult(ex.Message, HttpStatusCode.InternalServerError);
            }

            return new RedirectResult($"/Albums/Details?id={albumId}");
        }

        internal IHttpResponse Details(IHttpRequest request)
        {
            if (!base.IsAuthenticated(request))
                return new RedirectResult("/Users/Login");

            string albumId = request.QueryData["?albumId"].ToString();
            string trackId = request.QueryData["trackId"].ToString();

            if (!base.DbContext.Albums.Any(x => x.Id == albumId))
                return new RedirectResult("/Albums/All");

            Track track = base.DbContext.Tracks.FirstOrDefault(x => x.Id == trackId);

            if (track == null)
                return new RedirectResult($"/Albums/Details?id={albumId}");

            base.ViewBag["link"] = WebUtility.UrlDecode(track.Link);
            base.ViewBag["name"] = track.Name;
            base.ViewBag["price"] = $"{track.Price:f2}";
            base.ViewBag["albumId"] = albumId;

            return base.View();
        }
    }
}