using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Domain.VenueDomain.Model
{
    public class RoomEquipment : BaseEntity
    {
        public RoomEquipment(Guid? id, SelectValueMapping equipment, Room room, int? count, string description = null) : base(id)
        {
            Equipment = equipment;
            Room = room;
            Quantity = count;
            Description = description;
        }

        public RoomEquipment(Guid? id, CreateRoomEquipment.Command command) : base(id)
        {
            EquipmentId = command.EquipmentId;
            RoomId = command.RoomId;
            Quantity = command.Quantity;
            Description = command.Description;
        }

        public RoomEquipment()
        {
        }

        public void Update (ModifyRoomEquipment.Command command)
        {
            Quantity = command.Quantity;
            Description = command.Description;
        }
        
        public Guid? EquipmentId { get; private set; }
        public virtual SelectValueMapping Equipment { get; private set; }
        public Guid? RoomId { get; private set; }
        public virtual Room Room { get; private set; }
        public int? Quantity { get; private set; }
        public string Description { get; private set; }
    }
}