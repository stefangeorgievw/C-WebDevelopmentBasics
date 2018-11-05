using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TurshiaWebApp.Models;
using TurshiaWebApp.Models.Enums;
using TurshiaWebApp.ViewModels;

namespace TurshiaWebApp.Controllers
{
    public class TasksController:BaseController
    {
        [Authorize("Admin")]
        public IHttpResponse Create()
        {


            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IHttpResponse Create(DoCreateTaskInputModel model)
        {
            var task = new Task
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                Participants = model.Participants,
                

            };

            //if (model.AffectedSectors != null)
            //{
            //    foreach (var affectedSector in model.AffectedSectors)
            //    {
            //        task.AffectedSectors.Add(new TaskSector
            //        {
            //            Sector = Sectors
            //        });
            //    }
            //}
            return this.View();
        }
    }
}
