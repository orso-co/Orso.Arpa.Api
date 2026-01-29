using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.StageSetupDomain.Commands;

namespace Orso.Arpa.Domain.StageSetupDomain.Model
{
    /// <summary>
    /// Represents a stage setup (Aufstellung) for a project.
    /// Contains the PDF floor plan and participant positions.
    /// </summary>
    public class StageSetup : BaseEntity
    {
        public StageSetup(Guid? id, CreateStageSetup.Command command) : base(id)
        {
            ProjectId = command.ProjectId;
            Name = command.Name;
            FileName = command.FileName;
            StoragePath = command.StoragePath;
            ContentType = command.ContentType;
            FileSize = command.FileSize;
            CanvasWidth = command.CanvasWidth;
            CanvasHeight = command.CanvasHeight;
            IsActive = command.IsActive;
            IsVisibleToPerformers = command.IsVisibleToPerformers;
        }

        protected StageSetup()
        {
        }

        public void Update(ModifyStageSetup.Command command)
        {
            Name = command.Name;
            CanvasWidth = command.CanvasWidth;
            CanvasHeight = command.CanvasHeight;
            IsActive = command.IsActive;
            IsVisibleToPerformers = command.IsVisibleToPerformers;
        }

        /// <summary>
        /// Updates the PDF file information
        /// </summary>
        public void UpdateFile(string fileName, string storagePath, string contentType, long fileSize)
        {
            FileName = fileName;
            StoragePath = storagePath;
            ContentType = contentType;
            FileSize = fileSize;
        }

        /// <summary>
        /// Sets this setup as active and deactivates all others for the same project
        /// </summary>
        public void Activate()
        {
            IsActive = true;
        }

        /// <summary>
        /// Deactivates this setup
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }

        #region Properties

        /// <summary>
        /// Name of the stage setup (e.g., "Konzerthaus Berlin", "Open Air Festival")
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Original filename of the uploaded PDF
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Path to the stored PDF file in the filesystem
        /// </summary>
        public string StoragePath { get; private set; }

        /// <summary>
        /// MIME type (should be application/pdf)
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long FileSize { get; private set; }

        /// <summary>
        /// Reference width for position calculations (pixels)
        /// </summary>
        public int CanvasWidth { get; private set; }

        /// <summary>
        /// Reference height for position calculations (pixels)
        /// </summary>
        public int CanvasHeight { get; private set; }

        /// <summary>
        /// Whether this is the currently active setup for the project
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Whether performers can view this setup
        /// </summary>
        public bool IsVisibleToPerformers { get; private set; }

        #endregion

        #region Relationships

        /// <summary>
        /// The project this setup belongs to
        /// </summary>
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        /// <summary>
        /// Participant positions on this stage setup
        /// </summary>
        [CascadingSoftDelete]
        public virtual ICollection<StageSetupPosition> Positions { get; private set; } = new HashSet<StageSetupPosition>();

        /// <summary>
        /// Equipment items on this stage setup
        /// </summary>
        [CascadingSoftDelete]
        public virtual ICollection<StageSetupEquipment> Equipment { get; private set; } = new HashSet<StageSetupEquipment>();

        #endregion

        public override string ToString()
        {
            return $"{Name} ({Project?.Title ?? "Unknown Project"})";
        }
    }
}
