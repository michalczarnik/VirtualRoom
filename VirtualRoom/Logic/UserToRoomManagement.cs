using System;
using System.Collections.Generic;
using System.Linq;
using VirtualRoom.Models;

namespace VirtualRoom.Logic
{
    public static class UserToRoomManagement
    {
        private static Dictionary<Guid, List<UserModel>> roomToUserMap = new Dictionary<Guid, List<UserModel>>();
        private static IDatasource datasource = new FileDatasource();
        public static bool AddUserToRoom(Guid roomId, string userId)
        {
            if (roomToUserMap.ContainsKey(roomId))
            {
                if (roomToUserMap[roomId].Any(r => r.UserId.Equals(userId)))
                {
                    return false;
                }
                else
                {   
                    roomToUserMap[roomId].Add(datasource.GetUser(userId));
                    return true;
                }
            }
            else
            {
                var users = new List<UserModel>();
                users.Add(datasource.GetUser(userId));
                roomToUserMap.Add(roomId, users);
                return true;
            }
        }

        public static bool RemoveUserFromRoom(Guid roomId, string userId)
        {
            if (roomToUserMap.ContainsKey(roomId))
            {
                if (roomToUserMap[roomId].Any(r => r.UserId.Equals(userId)))
                {
                    roomToUserMap[roomId] = roomToUserMap[roomId].Where(_ => !_.UserId.Equals(userId)).ToList();
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<UserModel> GetAllUsersForRoom(Guid roomId)
        {
            if (roomToUserMap.ContainsKey(roomId))
            {
                return roomToUserMap[roomId];
            }
            return Enumerable.Empty<UserModel>();
        }
    }
}