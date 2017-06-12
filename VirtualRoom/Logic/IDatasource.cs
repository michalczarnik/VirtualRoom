using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRoom.Models;

namespace VirtualRoom.Logic
{
    interface IDatasource
    {
        IEnumerable<RoomModel> GetAllRooms();
        bool AddRoom(RoomModel Room);
        bool ChangeRoomInformation(Guid RoomID, string RoomName, string RoomDescription, int? Capacity, List<Guid> ExcludedUsers, string IconURI , Guid AdminID);
        RoomModel GetRoom(Guid RoomID);
        bool DeleteRoom(Guid RoomID);

        IEnumerable<UserModel> GetUsers();
        IEnumerable<UserModel> GetUsersInRoom(Guid roomId);
        UserModel GetUser(string userId);
    }
}
