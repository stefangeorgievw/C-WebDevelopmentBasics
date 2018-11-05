namespace IRunes.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;

    using IRunes.Models;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;

    public class AlbumsController : BaseController
    {
        public IHttpResponse All(IHttpRequest request)
        {
            if (!base.IsAuthenticated(request)) 
                return new RedirectResult("/Users/Login");

            var albums = base.DbContext.Albums;
            var albumsList = String.Empty;

            if (albums.Any())
            {
                foreach (var album in albums)
                {
                    string albumHtml = $@"<p><a href=""/Albums/Details?id={album.Id}"">{album.Name}</a></p>";
                    albumsList += albumHtml;
                }
            }
            else
            {
                albumsList = @"<p>There are currently no albums.</p>";
            }

            base.ViewBag["albumsList"] = albumsList;

            return base.View();
        }

        public IHttpResponse Create(IHttpRequest request)
        {
            if (!base.IsAuthenticated(request))
                return new RedirectResult("/Users/Login");

            return base.View();
        }

        public IHttpResponse DoCreate(IHttpRequest request)
        {
            string name = request.FormData["name"].ToString();
            string cover = request.FormData["cover"].ToString();

            if (String.IsNullOrWhiteSpace(name))
                return new RedirectResult("/Albums/Create");

            if (String.IsNullOrWhiteSpace(cover))
                return new RedirectResult("Albums/Create");

            if (base.DbContext.Albums.Any(x => x.Name == name))
                return new RedirectResult("Albums/Create");

            if (base.DbContext.Albums.Any(x => x.Cover == cover))
                return new RedirectResult("Albums/Create");

            var album = new Album
            {
                Name = name,
                Cover = cover                
            };

            base.DbContext.Albums.Add(album);

            try
            {
                base.DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HtmlResult(ex.Message, HttpStatusCode.InternalServerError);
            }

            return new RedirectResult("/Albums/All");
        }

        internal IHttpResponse Details(IHttpRequest request)
        {
            if (!base.IsAuthenticated(request))
                return new RedirectResult("/Users/Login");

            string id = request.QueryData["?id"].ToString();

            Album album = base.DbContext.Albums.FirstOrDefault(x => x.Id == id);

            if (album == null)
                return new RedirectResult("/Albums/All");

            base.ViewBag["cover"] = WebUtility.UrlDecode(album.Cover);
            base.ViewBag["name"] = album.Name;
            base.ViewBag["price"] = $"{album.Price:f2}";
            base.ViewBag["albumId"] = album.Id;

            int counter = 1;
            string tracksList = "<ul>";
            foreach (var track in album.Tracks)
            {
                string trackHtml =
                    $@"<li>{counter}. <a href=""/Tracks/Details?albumId={album.Id}&trackId={track.Id}"">{track.Name}</a></li>";
                tracksList += trackHtml;

                counter++;
            }
            tracksList += "</ul>";

            if (!album.Tracks.Any())
                tracksList += "There are currently no tracks.";

            base.ViewBag["tracksList"] = tracksList;

            return base.View();
        }
    }
}