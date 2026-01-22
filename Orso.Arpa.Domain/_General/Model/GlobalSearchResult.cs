using System;

namespace Orso.Arpa.Domain.General.Model
{
    /// <summary>
    /// Result from global search function across multiple entity types
    /// </summary>
    public class GlobalSearchResult
    {
        /// <summary>
        /// Type of entity (Person, Appointment, Project, News)
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// Unique identifier of the entity
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// Display name of the entity
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Additional information (e.g., instrument, date, venue)
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Relevance score (0-1, higher is better)
        /// </summary>
        public float Relevance { get; set; }
    }
}
