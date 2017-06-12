using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using VirtualRoom.Models;
using System.Xml.Linq;

namespace VirtualRoom.Logic
{
    public class FileDatasource : IDatasource
    {
        public bool AddRoom(RoomModel Room)
        {
            var rooms = GetAllRooms();
            var roomsList = rooms.ToList();
            if (rooms.Where(rM => rM.Name.Equals(Room.Name)).Any())
                return false;
            roomsList.Add(Room);
            try
            {
                SaveData(roomsList);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeRoomInformation(Guid RoomID, string RoomName, string RoomDescription, int? Capacity, List<Guid> ExcludedUsers, string IconURI, Guid AdminID)
        {
            var rooms = GetAllRooms();
            var room = GetRoom(RoomID);
            if (room == null)
                return false;
            room.Name = RoomName ?? room.Name;
            room.Description = RoomDescription ?? room.Description;
            room.Capacity = Capacity ?? room.Capacity;
            room.ExcludedUsers = ExcludedUsers ?? room.ExcludedUsers;
            room.IconURI = IconURI ?? room.IconURI;
            room.AdminID = Guid.Empty.Equals(AdminID)?room.AdminID:AdminID;
            if (rooms.Contains(room))
            {
                return false;
            }
            var roomsList = rooms.Where(rM => !rM.RoomID.Equals(RoomID)).ToList();
            roomsList.Add(room);
            try
            {
                SaveData(roomsList);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteRoom(Guid RoomID)
        {
            var rooms = GetAllRooms();
            var excludedRoom = rooms.Where(rM => rM.RoomID.Equals(RoomID)).FirstOrDefault();
            if (excludedRoom == null)
                return false;
            rooms = rooms.Where(rM => !rM.Equals(excludedRoom));
            try
            {
                SaveData(rooms);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<RoomModel> GetAllRooms()
        {
            XElement xml;
            try
            {
                xml = XElement.Load("C:\\TemporaryDatasource.xml");
            }
            catch (Exception e)
            {
                SaveData(CreateTestData());
                xml = XElement.Load("C:\\TemporaryDatasource.xml");
            }
            List<RoomModel> rooms = new List<RoomModel>();
            foreach (var room in xml.Descendants("room"))
            {
                RoomModel rM = new RoomModel();
                rM.Name = room.Descendants("name").FirstOrDefault().Value;
                rM.Description = room.Descendants("description").FirstOrDefault().Value;
                rM.Capacity = Int32.Parse(room.Descendants("capacity").FirstOrDefault().Attributes("value").FirstOrDefault().Value);
                rM.IconURI = room.Descendants("iconURI").FirstOrDefault().Value;
                rM.RoomID = Guid.Parse(room.Descendants("roomID").FirstOrDefault().Value);
                rM.AdminID = Guid.Parse(room.Descendants("adminID").FirstOrDefault().Value);
                var exclUsrs = room.Descendants("excludedUser");
                foreach (var usr in exclUsrs)
                {
                    var guid = Guid.Parse(usr.Value);
                    rM.ExcludedUsers.Add(guid);
                }
                rooms.Add(rM);
            }
            return rooms;
        }

        public RoomModel GetRoom(Guid RoomID)
        {
            var rooms = GetAllRooms();
            return rooms.Where(rM => rM.RoomID.Equals(RoomID)).FirstOrDefault();
        }

        public UserModel GetUser(string userId)
        {
            UserModel uM = null;
            //TODO Find user in DB, if not create anonymous one 

            uM = new UserModel
            {
                UserId = userId,
                Name = "Anonymous " + userId
            };
            return uM;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetUsersInRoom(Guid roomId)
        {
            return UserToRoomManagement.GetAllUsersForRoom(roomId);
        }

        private List<RoomModel> CreateTestData()
        {
            List<RoomModel> rooms = new List<RoomModel>();
            for (int i = 0; i < 10; i++)
            {
                RoomModel rM = new RoomModel();
                rM.Name = "Room" + (i + 1);
                rM.Description = "Room Description " + (i + 1);
                rM.AdminID = Guid.NewGuid();
                rM.Capacity = (i + 5) % 6;
                for (int j = 0; j < i % 3; j++)
                {
                    rM.ExcludedUsers.Add(Guid.NewGuid());
                }
                rooms.Add(rM);
            }
            return rooms;
        }

        private void SaveData(IEnumerable<RoomModel> rooms)
        {
            XElement roomsElement = new XElement("rooms");
            foreach (var room in rooms)
            {
                XElement roomElement = new XElement("room");
                XElement name = new XElement("name", room.Name);
                roomElement.Add(name);
                XElement description = new XElement("description", room.Description);
                roomElement.Add(description);
                XElement exclUsrs = new XElement("excludedUsers");
                if (room.ExcludedUsers.Any())
                {
                    foreach (var user in room.ExcludedUsers)
                    {
                        XElement excUsr = new XElement("excludedUser", user.ToString());
                        exclUsrs.Add(excUsr);
                    }
                }
                roomElement.Add(exclUsrs);
                XElement capacity = new XElement("capacity", new XAttribute("value", room.Capacity));
                roomElement.Add(capacity);
                XElement icon = new XElement("iconURI", room.IconURI);
                roomElement.Add(icon);
                XElement admin = new XElement("adminID", room.AdminID.ToString());
                roomElement.Add(admin);
                XElement roomID = new XElement("roomID", room.RoomID.ToString());
                roomElement.Add(roomID);
                roomsElement.Add(roomElement);
            }
            roomsElement.Save("C:\\TemporaryDatasource.xml");
        }
    }
}