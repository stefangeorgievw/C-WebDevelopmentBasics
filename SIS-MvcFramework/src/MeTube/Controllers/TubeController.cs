using MeTube.Models;
using MeTube.ViewModels.Tube;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MeTube.Controllers
{
    public  class TubeController :BaseController
    {
        [Authorize]
        public IHttpResponse Upload()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IHttpResponse Upload(UploadViewModel model)
        {
            var uploader = this.Db.Users.FirstOrDefault(x => x.Username == User.Username);
            if (uploader == null)
            {
                throw new BadRequestException("Invalid User");
            }

            var tube = new Tube
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                YoutubeId = model.YoutubeId,
                Uploader = uploader,
            };
            this.Db.Tubes.Add(tube);
            this.Db.SaveChanges();
            return this.Redirect("/");
        }

        [Authorize]
        public IHttpResponse Details(int id)
        {
            var tube = this.Db.Tubes.FirstOrDefault(x => x.Id == id);

            if (tube == null)
            {
                return BadRequestError("Incorrect tube");
            }

            tube.Views += 1;
            this.Db.SaveChanges();

            return this.View(tube);
        }
    }
}
