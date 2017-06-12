using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Web.Script.Serialization;
using VirtualRoom.Logic;
using VirtualRoom.Models;
using System.Net;

namespace VirtualRoom.Controllers
{
    public class RoomApiController : ApiController
    {
        FileDatasource rFD;
        JavaScriptSerializer serializer;
        public RoomApiController()
        {
            rFD = new FileDatasource();
            serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        }

        [ActionName("GetAll")]
        public string GetAll()
        {
            rFD = new FileDatasource();
            return serializer.Serialize(rFD.GetAllRooms()); 
        }

        [HttpPost]
        [ActionName("AddRoom")]
        public HttpResponseMessage AddRoom([FromBody] dynamic value)
        {
            var objectParsed = (JObject)value;
            rFD = new FileDatasource();
            RoomModel room = new RoomModel();
            Guid adminId = Guid.Empty;
            if (Guid.TryParse(objectParsed.Value<string>("adminid"), out adminId))
            {
                room.AdminID = adminId;
                if (objectParsed.Value<string>("name") != null)
                {
                    room.Name = objectParsed.Value<string>("name");
                    room.Description = objectParsed.Value<string>("description");
                    int capacity = 0;
                    if (objectParsed["capacity"] != null )
                        room.Capacity = objectParsed.Value<int>("capacity");
                    room.IconURI = objectParsed.Value<string>("iconuri");
                    room.ExcludedUsers = ExtensionMethods.ParseUsers(objectParsed);
                    if (rFD.AddRoom(room))
                    {
                        return this.Request.CreateResponse(HttpStatusCode.OK);
                    }
                    return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
                }
                else
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No name for the room is provided");
                }
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Admin ID is in incorrect format");
            
        }

        [HttpPost]
        [ActionName("RemoveRoom")]
        public HttpResponseMessage RemoveRoom([FromBody] dynamic value)
        {
            var parsedValue = (JObject)value;
            Guid id = Guid.Empty;
            if (Guid.TryParse(parsedValue.Value<string>("roomid"), out id))
            {
                if (rFD.DeleteRoom(id))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
            return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "ID is in incorrect format");
        }

        [HttpPost]
        [ActionName("ChangeRoom")]
        public HttpResponseMessage ChangeRoom([FromBody] dynamic value)
        {
            var objectParsed = (JObject)value;
            if (objectParsed["roomid"] != null)
            {
                rFD = new FileDatasource();
                Guid id;
                if (Guid.TryParse(objectParsed.Value<string>("roomid"), out id))
                {
                    Guid adminId = Guid.Empty;
                    if (objectParsed.Value<string>("adminid") != null)
                    {
                        Guid.TryParse(objectParsed.Value<string>("adminid"), out adminId);
                    }
                    if (rFD.ChangeRoomInformation(id, objectParsed.Value<string>("name"), objectParsed.Value<string>("description"), objectParsed.Value<int>("capacity"), ExtensionMethods.ParseUsers(objectParsed), objectParsed.Value<string>("iconuri"), adminId))
                    {
                        return this.Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                        return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "There is no room with id: " + id.ToString());
                }
                else
                    return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "ID is in incorrect format");
            }
            else
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No ID is provided");
            }
        }

    }
}
