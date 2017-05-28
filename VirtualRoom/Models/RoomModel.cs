using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualRoom.Models
{
    public class RoomModel
    {
        private Guid _RoomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconURI { get; set; }
        public List<Guid> ExcludedUsers = new List<Guid>();
        public Guid AdminID { get; set; }
        public int Capacity { get; set; }
        public Guid RoomID
        {
            get
            {
                if (Guid.Empty.Equals(_RoomId))
                {
                    _RoomId = Guid.NewGuid();
                }
                return _RoomId;
            }
            set
            {
                _RoomId = value;
            }
        }
    }
}