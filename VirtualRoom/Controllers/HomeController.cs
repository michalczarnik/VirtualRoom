using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualRoom.Logic;
using VirtualRoom.Models;

namespace VirtualRoom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult RoomListing()
        {
            RoomFileDatasource rmd = new RoomFileDatasource();
            var rooms = rmd.GetAllRooms();
            rmd.DeleteRoom(Guid.Parse("e7095f2d-7a75-4ecc-af26-cebcc5adaeac"));
            return View(rooms);
        }

        public ActionResult Room(string id)
        {
            RoomFileDatasource rFD = new RoomFileDatasource();
            var room = rFD.GetRoom(Guid.Parse(id));
            return View(room);
        }
    }
}