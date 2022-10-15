using System;
using System.Configuration;
using System.Globalization;
using System.Runtime.Serialization;

namespace Shared.Core.Model.Core
{
    /// <summary>
    /// Base class of all DTO.
    /// </summary>
    [DataContract]
    public abstract class EntityBase
    {
        /// <summary>
        /// Entity's identifier.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// StructureId's identifier.
        /// </summary>
        [DataMember]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Indicates whether or not the entity is editable.
        /// </summary>
        [DataMember]
        public bool? ReadOnly { get; set; }

        /// <summary>
        /// Indicates whether or not the entity has an error.
        /// </summary>
        [DataMember]
        public bool? HasError { get; set; }

        /// <summary>
        /// Indicates whether or not the entity is selected.
        /// </summary>
        [DataMember]
        public bool Selected { get; set; }

        /// <summary>
        ///  Indicates whether the entity is active.
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        ///  Entity's LastModificationDate.
        /// </summary>
        [IgnoreDataMember]
        public DateTime? LastModificationDate { get; set; }

        /// <summary>
        /// Entity's LastModificationDate.
        /// </summary>
        /// 
        [DataMember]
        public string LastModificationDateString { get; set; }

        /// <summary>
        /// Entity's LastModificationDateTime.
        /// </summary>
        /// 
        [DataMember]
        public string LastModificationDateTimeString { get; set; }

        public DateTime? Parse(string inputDate)
        {
            if (string.IsNullOrWhiteSpace(inputDate))
            {
                return null;
            }

            string[] splitted = inputDate.Split(new char[] { '/', '-' });

            if (inputDate.Contains("-"))
            {
                return new DateTime(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]), Convert.ToInt32(splitted[2]));

            }
            else
            {
                return new DateTime(Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[1]), Convert.ToInt32(splitted[0]));
            }
        }

        [OnDeserialized]
        private void OnDeserializing(StreamingContext context)
        {
            try
            {
                string dateformat = ConfigurationManager.AppSettings["dateformat"];

                this.LastModificationDate = null;
                if (!string.IsNullOrWhiteSpace(this.LastModificationDateString))
                {
                    this.LastModificationDate = DateTime.ParseExact(this.LastModificationDateString, dateformat, CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
            }
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            try
            {

                if (this.LastModificationDate == null)
                {
                    this.LastModificationDateString = "";
                    this.LastModificationDateTimeString = "";
                }
                else
                {
                    string dateformat = ConfigurationManager.AppSettings["dateformat"];
                    string datetimeformat = ConfigurationManager.AppSettings["datetimeformat"];

                    this.LastModificationDateTimeString = this.LastModificationDate.Value.ToString(datetimeformat, CultureInfo.InvariantCulture);
                    this.LastModificationDateString = this.LastModificationDate.Value.ToString(dateformat, CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
            }
        }

        [DataMember]
        public string DateFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["dateformat"];
            }

            private set { }
        }

        public override string ToString()
        {
            return $"Id: {this.Id} | ApplicationId : {this.ApplicationId} | Modifié le : {this.LastModificationDate?.ToLongDateString()}";
        }

        /// <summary>
        /// Parses a given date string using the specified separator;
        /// </summary>
        /// <param name="inputDate">Date to parse</param>
        /// <param name="separator">Separator to user</param>
        /// <returns>Parse date</returns>
        public static DateTime? Parse(string inputDate, char separator)
        {
            if (string.IsNullOrWhiteSpace(inputDate))
            {
                return null;
            }

            string[] splitted = inputDate.Split(new char[] { separator });

            return new DateTime(Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[1]), Convert.ToInt32(splitted[0]));
        }
    }
}