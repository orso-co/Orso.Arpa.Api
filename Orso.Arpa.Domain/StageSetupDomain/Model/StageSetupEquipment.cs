using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.StageSetupDomain.Commands;

namespace Orso.Arpa.Domain.StageSetupDomain.Model
{
    /// <summary>
    /// Represents an equipment item's position on a stage setup.
    /// Coordinates are stored as percentages (0-100) relative to the canvas dimensions.
    /// </summary>
    public class StageSetupEquipment : BaseEntity
    {
        public StageSetupEquipment(Guid? id, CreateStageSetupEquipment.Command command) : base(id)
        {
            StageSetupId = command.StageSetupId;
            EquipmentId = command.EquipmentId;
            PositionX = command.PositionX;
            PositionY = command.PositionY;
            Rotation = command.Rotation;
        }

        protected StageSetupEquipment()
        {
        }

        public void Update(double positionX, double positionY, double rotation)
        {
            PositionX = positionX;
            PositionY = positionY;
            Rotation = rotation;
        }

        #region Properties

        /// <summary>
        /// Equipment type identifier (e.g., "steinway-b", "thonet-chair")
        /// </summary>
        public string EquipmentId { get; private set; }

        /// <summary>
        /// X coordinate as percentage (0-100) of canvas width
        /// </summary>
        public double PositionX { get; private set; }

        /// <summary>
        /// Y coordinate as percentage (0-100) of canvas height
        /// </summary>
        public double PositionY { get; private set; }

        /// <summary>
        /// Rotation in degrees (0-360)
        /// </summary>
        public double Rotation { get; private set; }

        #endregion

        #region Relationships

        /// <summary>
        /// The stage setup this equipment belongs to
        /// </summary>
        public Guid StageSetupId { get; private set; }
        public virtual StageSetup StageSetup { get; private set; }

        #endregion
    }
}
