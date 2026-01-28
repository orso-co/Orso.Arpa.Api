using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.StageSetupDomain.Commands;

namespace Orso.Arpa.Domain.StageSetupDomain.Model
{
    /// <summary>
    /// Represents a participant's position on a stage setup.
    /// Coordinates are stored as percentages (0-100) relative to the canvas dimensions.
    /// </summary>
    public class StageSetupPosition : BaseEntity
    {
        public StageSetupPosition(Guid? id, CreateStageSetupPosition.Command command) : base(id)
        {
            StageSetupId = command.StageSetupId;
            MusicianProfileId = command.MusicianProfileId;
            PositionX = command.PositionX;
            PositionY = command.PositionY;
            Row = command.Row;
            Stand = command.Stand;
        }

        protected StageSetupPosition()
        {
        }

        public void Update(ModifyStageSetupPosition.Command command)
        {
            PositionX = command.PositionX;
            PositionY = command.PositionY;
            Row = command.Row;
            Stand = command.Stand;
        }

        /// <summary>
        /// Updates only the position coordinates (for drag-drop operations)
        /// </summary>
        public void UpdatePosition(double positionX, double positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        #region Properties

        /// <summary>
        /// X coordinate as percentage (0-100) of canvas width
        /// </summary>
        public double PositionX { get; private set; }

        /// <summary>
        /// Y coordinate as percentage (0-100) of canvas height
        /// </summary>
        public double PositionY { get; private set; }

        /// <summary>
        /// Optional row number (for orchestral seating)
        /// </summary>
        public int? Row { get; private set; }

        /// <summary>
        /// Optional stand/desk number (for string sections sharing a stand)
        /// </summary>
        public int? Stand { get; private set; }

        #endregion

        #region Relationships

        /// <summary>
        /// The stage setup this position belongs to
        /// </summary>
        public Guid StageSetupId { get; private set; }
        public virtual StageSetup StageSetup { get; private set; }

        /// <summary>
        /// The musician profile (participant) at this position
        /// </summary>
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }

        #endregion

        public override string ToString()
        {
            return $"Position ({PositionX:F1}%, {PositionY:F1}%) - {MusicianProfile?.Person?.DisplayName ?? "Unknown"}";
        }
    }
}
