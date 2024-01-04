using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.VenueDomain.Model
{
    public class RoomEquipment : BaseEntity
    {
        public RoomEquipment(Guid? id, SelectValueMapping equipment, Room room) : base(id)
        {
            Equipment = equipment;
            Room = room;
        }

        public RoomEquipment(Guid equipmentId, Guid roomId)
        {
            EquipmentId = equipmentId;
            RoomId = roomId;
        }

        public RoomEquipment()
        {
        }
        
        public Guid? EquipmentId { get; private set; }
        public virtual SelectValueMapping Equipment { get; private set; }
        public Guid? RoomId { get; private set; }
        public virtual Room Room { get; private set; }
        public int Count { get; private set; }
        public string Description { get; private set; }
    }
}