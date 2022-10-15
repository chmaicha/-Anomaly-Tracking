using Shared.Core.Model.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Shared.Core.Model.Filters
{
    /// <summary>
    /// Represents <code>SearchFilterBase</code> object
    /// </summary>
    [DataContract]
    public class SearchFilterBase
    {
        /// <summary>
        /// Contains the connected user associated application id.
        /// </summary>
        [DataMember]
        public int? ApplicationId { get; set; }

        /// <summary>
        /// Contains the application uid.
        /// </summary>
        [DataMember]
        public string ApplicationUID { get; set; }

        /// <summary>
        /// Indicates wheter or not the connected user is an admin.
        /// </summary>
        [DataMember]
        public bool? IsAdmin { get; set; }

        /// <summary>
        /// Contains the connected user identifier.
        /// </summary>
        [DataMember]
        public int? ConnectedUserId { get; set; }

        /// <summary>
        /// Indicates whether or not it should get only active entities.
        /// </summary>
        [DataMember]
        public bool? OnlyActiveEntities { get; set; }

        /// <summary>
        /// Indicates whether or not it should get only entity in use.
        /// </summary>
        [DataMember]
        public bool? AlreadyInUse { get; set; }

        /// <summary>
        /// Contains the index of the actual page to get.
        /// </summary>
        [DataMember]
        public int Page { get; set; }

        /// <summary>
        /// Contains the number of elements to get for the actual page.
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// Contains the total number of existing items into database.
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }

        /// <summary>
        /// Indicates whether or not it should paginate data.
        /// </summary>
        [DataMember]
        public bool Paginate { get; set; }

        /// <summary>
        /// Search input.
        /// </summary>
        [DataMember]
        public string SearchInput { get; set; }

        /// <summary>
        /// Search input for the searchbar toolbar of each module.
        /// </summary>
        [DataMember]
        public string GlobalSearchInput { get; set; }

        /// <summary>
        /// Indicates whether the user is just connecting to application.
        /// </summary>
        [DataMember]
        public bool IsStartUp { get; set; }

        /// <summary>
        /// Selected years.
        /// </summary>
        [DataMember]
        public IEnumerable<int> Years { get; set; }

        /// <summary>
        /// Selected months.
        /// </summary>
        [DataMember]
        public IEnumerable<int> Months { get; set; }

        /// <summary>
        /// Contains the list of entities'ids to get.
        /// </summary>
        [DataMember]
        public IEnumerable<int> EntityIds { get; set; }

        /// <summary>
        /// Contains the list of third parts to filter by.
        /// </summary>
        [DataMember]
        public IEnumerable<int> ThirdPartIds { get; set; }

        /// <summary>
        /// Contains the list of users to filter by.
        /// </summary>
        [DataMember]
        public IEnumerable<int> UserIds { get; set; }

        /// <summary>
        /// Contains the list of statuses to get the entities for.
        /// </summary>
        [DataMember]
        public IEnumerable<int> Statuses { get; set; }

        /// <summary>
        /// Ensures to exclude theses entities from the response. It contains should a list of identifiers.
        /// </summary>
        [DataMember]
        public IEnumerable<int> Excepts { get; set; }

        /// <summary>
        /// Allows to filter by start date.
        /// </summary>
        [IgnoreDataMember]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Allows to filter by start date.
        /// </summary>
        [DataMember]
        public string StartDateString { get; set; }

        /// <summary>
        /// Allows to filter by end date.
        /// </summary>
        [IgnoreDataMember]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Allows to filter by end date.
        /// </summary>
        [DataMember]
        public string EndDateString { get; set; }

        /// <summary>
        /// Allows to filter by current date.
        /// </summary>
        [DataMember]
        public bool? ByActualDate { get; set; }

        [OnDeserialized]
        private void OnDeserializing(StreamingContext context)
        {
            try
            {
                string dateformat = ConfigurationManager.AppSettings["dateformat"];

                this.StartDate = EntityBase.Parse(this.StartDateString, '/');

                if (!string.IsNullOrWhiteSpace(this.EndDateString))
                {
                    this.EndDate = EntityBase.Parse(this.EndDateString, '/');
                }
            }
            catch (Exception)
            {
            }
        }
    }
}