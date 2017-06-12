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
            FileDatasource rmd = new FileDatasource();
            var rooms = rmd.GetAllRooms();
            return View(rooms);
        }

        public ActionResult Room(string id)
        {
            FileDatasource rFD = new FileDatasource();
            var room = rFD.GetRoom(Guid.Parse(id));
            UserToRoomManagement.AddUserToRoom(Guid.Parse(id), Session.SessionID);
            return View(room);
        }
    }
}